using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class ProductNotFile
    {
        [AttributeCustomId]
        public int idproduct { get; set; }

        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Mã hàng hóa")]
        public string productcode { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string productname { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Nhóm hàng hóa
        /// </summary>
        public int? categoryid { get; set; }

        /// <summary>
        /// NHóm hàng hóa
        /// </summary>
        public string categoryname { get; set; }
        /// <summary>
        /// Giá vốn
        /// </summary>
        public int? costprice { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        public int? sellprice { get; set; }

        /// <summary>
        /// link anh chính
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// color
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// size
        /// </summary>
        public string size { get; set; }
    }
}
