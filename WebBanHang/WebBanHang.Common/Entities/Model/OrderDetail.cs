using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class OrderDetail
    {
        [AttributeCustomId]
        public int idorderdetail { get; set; }

        public int idsaleorder { get; set; }

        public int idproductdetail { get; set; }

        public int quantity { get; set; }

        public string size { get; set; }

        public string color { get; set; }

        public int sellprice { get; set; }

        public string productcode { get; set; }

        public string productname { get; set; }
    }
}
