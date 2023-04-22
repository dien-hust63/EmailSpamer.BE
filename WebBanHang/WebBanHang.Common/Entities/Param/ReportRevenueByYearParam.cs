using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanHang.Common.Entities.Param
{
    public class ReportRevenueByYearParam
    {
        /// <summary>
        /// id chi nhánh
        /// </summary>
        public int branchid { get; set; }

        /// <summary>
        /// năm
        /// </summary>
        public int year { get; set; }
    }
}
