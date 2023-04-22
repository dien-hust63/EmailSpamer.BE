using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gather.ApplicationCore.Entities.Param
{
    public class PagingRequest
    {
        /// <summary>
        /// index trang
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// số bản ghi trên 1 trang
        /// </summary>
        public int PageSize { get; set; }
    }
}
