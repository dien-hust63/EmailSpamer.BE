using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.BL
{
    public interface IReceiverBL : IBaseBL<Receiver>
    {

        ServiceResult importReceiver(IFormFile formfile);
    }
}
