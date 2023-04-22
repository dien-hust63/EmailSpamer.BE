using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class ProductDetail
    {
        [AttributeCustomId]
        public int idproductdetail { get; set; }

        /// <summary>
        /// id sản phẩm cha
        /// </summary>
        public int idproduct { get; set; }

        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Mã hàng hóa")]
        public string productcode { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string productname { get; set; }

        /// <summary>
        /// Giá vốn
        /// </summary>
        public int? costprice { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        public int? sellprice { get; set; }

        /// <summary>
        /// color
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// size
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// Tồn kho
        /// </summary>
        public int? inventory { get; set; }
    }
}
