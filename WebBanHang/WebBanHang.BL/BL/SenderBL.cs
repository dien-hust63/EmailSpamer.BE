using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Services;

namespace WebBanHang.BL.BL
{
    public class SenderBL : BaseBL<Sender>, ISenderBL
    {
        ISenderDL _senderDL;

        public SenderBL(IBaseDL<Sender> baseDL, ISenderDL senderDL) : base(baseDL)
        {
            _senderDL = senderDL;
        }
    }
}
