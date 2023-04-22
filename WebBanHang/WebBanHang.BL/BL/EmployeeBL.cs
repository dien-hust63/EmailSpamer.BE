using Gather.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Library;
using WebBanHang.Common.Services;
using WebBanHang.DL.DL;

namespace WebBanHang.BL.BL
{
    public class EmployeeBL:BaseBL<Employee>,IEmployeeBL
    {
        IEmployeeDL _employeeDL;
        IMailBL _mailBL;
        IConfiguration _configuration;
        public EmployeeBL(IBaseDL<Employee> baseDL, IEmployeeDL employeeDL, IMailBL mailBL, IConfiguration configuration) : base(baseDL)
        {
            _employeeDL = employeeDL;
            _mailBL = mailBL;
            _configuration = configuration;
    }

        /// <summary>
        /// Kích hoạt tài khoản
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult activeAccount(Employee employee)
        {
            ServiceResult serviceResult = new ServiceResult();
            // chuyển trạng thái
            string passRandom = RandomClass.GetRandomString(8);
            byte[] desKey = Convert.FromBase64String(_configuration.GetSection("DES").GetSection("Key").Value ?? "");
            byte[] desVI = Convert.FromBase64String(_configuration.GetSection("DES").GetSection("VI").Value ?? "");
            // encrypt password to save in database
            employee.password = DESUtility.Encrypt(passRandom, desKey, desVI);
            bool isUpdate = _employeeDL.activeAccount(employee);
            try
            {
                Task.Run(() => sendEmailActive(employee, passRandom));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            serviceResult.Data = isUpdate;
            return serviceResult;
        }

        /// <summary>
        /// Ngừng kích hoạt tài khoản
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult deactiveAccount(Employee employee)
        {
            ServiceResult serviceResult = new ServiceResult();
            bool isUpdate = _employeeDL.deactiveAccount(employee);
            try
            {
                Task.Run(() => sendEmailDeActive(employee));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            serviceResult.Data = isUpdate;
            return serviceResult;
        }

        /// <summary>
        /// Gửi email kích hoạt với mật khẩu mặc định
        /// </summary>
        /// <param name="employee"></param>
        public void sendEmailActive(Employee employee, string passRandom)
        {
            // gửi email mật khẩu
            
            MailRequest mailContent = new MailRequest();
            mailContent.ToEmail = employee.email;
            mailContent.Subject = "Thông báo kích hoạt tài khoản truy cập 360 for men.";
            string bodyContent = "<div>Chào mừng bạn đến với 360ForMen.<br/>Để truy cập trang quản lý, bạn đăng nhập với thông tin:<br/>Tài khoản {0}<br/>Mật khẩu mặc định:{1}<br/>Đường dẫn:{2}</div>";
            mailContent.Body = string.Format(bodyContent, employee.email, passRandom, "https://360formen.netlify.app/management/login");
            
            _mailBL.sendEmail(mailContent);
        }

        /// <summary>
        /// Gửi email ngừng kích hoạt 
        /// </summary>
        /// <param name="employee"></param>
        public void sendEmailDeActive(Employee employee)
        {
            MailRequest mailContent = new MailRequest();
            mailContent.ToEmail = employee.email;
            mailContent.Subject = "Thông báo ngừng kích hoạt tài khoản truy cập 360 for men.";
            string bodyContent = "Tài khoản của bạn bị ngừng kích hoạt. Vui lòng liên hệ quản trị hệ thống để biết thêm chi tiết.";
            mailContent.Body = bodyContent;
            _mailBL.sendEmail(mailContent);
        }
        /// <summary>
        /// Lấy thoogn tin nhân viên theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee GetEmployeeInfoByEmail(string email) 
        {
            Employee employee = _employeeDL.GetEmployeeInfoByEmail(email);
            return employee;
        }

    }
}
