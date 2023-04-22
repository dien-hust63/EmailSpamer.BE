using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Common.Entities
{
    public class OrderBy
    {
        public int SortOrder { get; set; }

        public string FieldName { get; set; }

        public OrderByType MyProperty { get; set; }
    }
}
