using Gather.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;

namespace WebBanHang.BL.BL
{
    public class AuthenticationBL : IAuthenticationBL
    {
        IAuthenticationDL _authenDL;
        IEmployeeBL _employeeBL;
        IConfiguration _config;
        public AuthenticationBL(IAuthenticationDL authenDL, IConfiguration config, IEmployeeBL employeeBL) 
        {
            _authenDL = authenDL;
            _config = config;
            _employeeBL = employeeBL;
        }
        /// <summary>
        /// kiểm tra tài khoản có hợp lệ không
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool checkUserAccount(LoginParam employee)
        {
            Employee employeeInfo = _authenDL.getEmployeeByEmail(employee.email);
            if(employeeInfo != null)
            {
                byte[] desKey = Convert.FromBase64String(_config.GetSection("DES").GetSection("Key").Value ?? "");
                byte[] desVI = Convert.FromBase64String(_config.GetSection("DES").GetSection("VI").Value ?? "");
                string passBase64 = DESUtility.Encrypt(employee.password, desKey, desVI);
                if (employeeInfo.password.Equals(passBase64))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Lấy danh sách các quyền của người dùng theo vai trò đối với các module
        /// </summary>
        /// <returns></returns>
        public List<RoleModule> GetListRoleModuleByUser(string email)
        {
            // Lấy được thông tin user 
            List<RoleModule> listRoleModule = _authenDL.GetListRoleModuleByUser(email);
            return listRoleModule;
        }

        /// <summary>
        /// Lấy thông tin user đăng nhập
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isemployee"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserInfo GetUserInfo(string email, bool isemployee = true)
        {
            UserInfo userInfo = new UserInfo();
            if (isemployee)
            {
                Employee employee = _employeeBL.GetEmployeeInfoByEmail(email);
                if (employee != null)
                {
                    userInfo.iduser = employee.idemployee;
                    userInfo.username = employee.employeename;
                    userInfo.email = employee.email;
                    userInfo.branchid= employee.branchid;
                    userInfo.branchname= employee.branchname;
                    userInfo.isemployee = isemployee;
                    userInfo.roleid = employee.roleid;
                    userInfo.rolename = employee.rolename;
                    userInfo.usercode = employee.employeecode;
                    userInfo.statusid = employee.statusid;
                    userInfo.statusname = employee.statusname;
                }
            }
            return userInfo;
        }

        public ServiceResult loginManagementApplication(Employee employee)
        {
            throw new NotImplementedException();
        }

        public ServiceResult registerManagementApplication(Employee employee)
        {
            var serviceResult = new ServiceResult();
            byte[] desKey = Convert.FromBase64String(_config.GetSection("DES").GetSection("Key").Value??"");
            byte[] desVI = Convert.FromBase64String(_config.GetSection("DES").GetSection("VI").Value ?? "");
            // encrypt password to save in database
            employee.password = DESUtility.Encrypt(employee.password, desKey, desVI);
            bool regisData =  _authenDL.registerManagementApplication(employee);
            serviceResult.Data = regisData;
            if (!regisData)
            {
                serviceResult.setError("Register fail");
            }
            return serviceResult;
        }
    }
}
