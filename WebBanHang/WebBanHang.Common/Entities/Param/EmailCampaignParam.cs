using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanHang.Common.Entities
{
    public class EmailCampaignParam
    {
        public bool IsAll { get; set; } = true;

        public List<int>? ListEmailID { get; set; }

        public int CampaignID { get; set; }
    }
}
