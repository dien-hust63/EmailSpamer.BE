using Gather.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Services;

namespace WebBanHang.BL.BL
{
    public class MailBL : BaseBL<MailRequest>, IMailBL
    {
        IMailDL _mailDL;
        private IConfiguration _configuration;

        public MailBL(IBaseDL<MailRequest> baseDL, IMailDL mailDL, IConfiguration configuration) : base(baseDL)
        {
            _mailDL = mailDL;
            _configuration = configuration;
        }

        /// <summary>
        /// Gửi email
        /// </summary>
        /// <param name="mailContent"></param>
        /// <returns></returns>
        public ServiceResult sendEmail(MailRequest mailContent)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                var mailSetting = _configuration.GetSection("MailSettings");
                MailMessage mail = new MailMessage();
                mail.To.Add(mailContent.ToEmail);
                mail.From = new MailAddress(mailContent.FromEmail, mailContent.FromDisplayName);
                mail.Subject = mailContent.Subject;
                mail.Body = mailContent.Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();

                smtp.Port = int.Parse(mailSetting["Port"]);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = mailSetting["Host"];
                smtp.Credentials = new NetworkCredential(mailContent.FromEmail, mailContent.FromEmailPassWord);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;

        }



    }
}
