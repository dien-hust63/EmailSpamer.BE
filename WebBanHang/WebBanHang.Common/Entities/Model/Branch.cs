using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class Branch
    {
        [AttributeCustomId]
        public int idbranch { get; set; }

        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Mã chi nhánh")]
        public string branchcode { get; set; }

        public string  branchname { get; set; }

        public string address { get; set; }

        public int? branchmanagerid { get; set; }

        public string branchmanagername { get; set; }

        public int? provinceid { get; set; }

        public string provincename { get; set; }

        public int? districtid { get; set; }

        public string districtname { get; set; }

        public string wardid { get; set; }

        public string wardname { get; set; }

        /// <summary>
        /// Sử dụng làm địa chỉ mặc định tính giá vận chuyển online
        /// </summary>
        public bool isaddressdefault { get; set; }
    }
}
