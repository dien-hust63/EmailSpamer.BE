using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class ProductCategory
    {
        [AttributeCustomId]
        public int idproductcategory { get; set; }

        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Mã nhóm hàng hóa")]
        public string productcategorycode { get; set; }

        public string productcategoryname { get; set; }

        public string description { get; set; }

        public int parentid { get; set; }

        public string parentname { get; set; }
    }
}
