using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Common.Entities
{
    public class BasePagingParam
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// Danh sách điều kiện lọc
        /// </summary>
        public List<FilterObject> ListFilter { get; set; }

        /// <summary>
        /// Công thức bộ lọc
        /// </summary>
        public string FilterFormula { get; set; }

        public List<OrderBy> ListOrderBy { get; set; }

        public string TableName { get; set; }
    }
}
