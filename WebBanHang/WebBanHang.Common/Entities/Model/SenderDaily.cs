using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class CommonSetting
    {
        public int idcommonsetting { get; set; }

        public int maxemail { get; set; }

        public string identify { get; set; } = "SETTING1";

    }
}
