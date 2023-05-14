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
using WebBanHang.Common.Entities;

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

        [HttpPost("paging")]
        public override IActionResult GetEntityPaging(BasePagingParam param)
        {
            try
            {
                var serviceResult = _senderBL.GetEntityPaging(param);
                //4.Trả về kết quả cho client
                if (serviceResult.Data != null)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Common.Resources.ResourceVN.Exception_ErrorMsg,
                    errorCode = "Gather-001",
                    moreInfo = "https://openapi.Gather.com.vn/errorcode/Gather-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }


        }
    }
}
