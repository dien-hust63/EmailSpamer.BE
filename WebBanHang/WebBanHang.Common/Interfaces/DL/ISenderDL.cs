﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.DL
{
    public interface ISenderDL : IBaseDL<Sender>
    {

        public List<SenderDaily> getSenderToday();

        public BasePagingResponse<Sender> getSenderPaging(BasePagingParam param);
    }
}
