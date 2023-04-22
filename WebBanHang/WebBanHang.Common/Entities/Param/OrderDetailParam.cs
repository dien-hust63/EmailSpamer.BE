using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanHang.Common.Entities.Param
{
    public class OrderDetailParam
    {
        /// <summary>
        /// master đơn hàng
        /// </summary>
        public string order { get; set; }

        /// <summary>
        /// detail đơn hàng
        /// </summary>
        public string orderdetail { get; set; }
    }
}
