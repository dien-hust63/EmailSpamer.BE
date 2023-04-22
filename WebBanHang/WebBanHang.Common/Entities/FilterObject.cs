using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Common.Entities
{
    public class FilterObject
    {
        public string FieldName { get; set; }

        public Operator Operator { get; set; }

        public string FilterValue { get; set; }

    }
}
