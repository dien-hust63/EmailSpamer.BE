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
    public class ReceiverController : BaseEntityController<Receiver>
    {
        IBaseBL<Receiver> _baseBL;
        IReceiverBL _receiverBL;
        public ReceiverController(IBaseBL<Receiver> baseBL, IReceiverBL receiverBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _receiverBL = receiverBL;
        }

        [HttpPost("import")]
        public ServiceResult importReceiver([FromForm] IFormFile file)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _receiverBL.importReceiver(file);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

    }
}
