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
    public class SenderDaily
    {
        public int idsenderdaily { get; set; }

        public int senderid { get; set; }

        public DateTime currentdate { get; set; }

        public int sentnumber { get; set; }

        [AttributeCustomNotMap]
        public string senderemail { get; set; }

    }
}
