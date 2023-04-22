using Gather.ApplicationCore.Entities;
using Gather.ApplicationCore.Entities.Param;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;

namespace WebBanHang.Api.Controllers
{
    [Authorize]
    public class EmailController : BaseEntityController<MailRequest>
    {
        IBaseBL<MailRequest> _baseBL;
        IMailBL _mailBL;
        public EmailController(IBaseBL<MailRequest> baseBL, IMailBL mailBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _mailBL = mailBL;
        }

        [HttpPost("send")]
        public ServiceResult sendEmail(MailRequest mailContent)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _mailBL.sendEmail(mailContent);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }


    }
}
