using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;

using Gather.ApplicationCore.Entities;
using Gather.ApplicationCore.Entities.Param;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;

namespace WebBanHang.Api.Controllers
{
    public class SenderController : BaseEntityController<Sender>
    {
        IBaseBL<Sender> _baseBL;
        ISenderBL _senderBL;
        public SenderController(IBaseBL<Sender> baseBL, ISenderBL senderBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _senderBL = senderBL;
        }

        [HttpGet("getSenderToday")]
        public ServiceResult getSenderToday()
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult.Data = _senderBL.getSenderToday();
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }
    }
}
