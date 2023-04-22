using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanHang.Common.Entities.Param
{
    public class ProductByCategoryParam
    {
        public int? categoryid { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int SortBy { get; set; }

        public string searchtext { get; set; }
    }
}
