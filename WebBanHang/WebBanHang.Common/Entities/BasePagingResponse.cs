using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Common.Entities
{
    public class BasePagingResponse<TEntity>
    {
        /// <summary>
        /// Danh sách dữ liệu
        /// </summary>
        public List<TEntity>  listPaging { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int Total { get; set; }
    }
}
 