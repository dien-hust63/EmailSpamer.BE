using Gather.ApplicationCore.Constant;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Common.Entities.Model
{
    public class CampaignDetail
    {
        [AttributeCustomId]
        public int idcampaigndetail { get; set; }

        public int idcampaign{ get; set; }

        public int idreceiver { get; set; }

        public string receiver { get; set; }

        public int statusid { get; set; }

        public string statusname
        {
            get
            {
                foreach (EmailCampaignStatus foo in Enum.GetValues(typeof(EmailCampaignStatus)))
                {
                    if (statusid == (int)foo)
                    {
                        return foo.GetDisplayName();
                    }
                }
                return "";
            }
            set {
               
            }
        }

        public DateTime? senddate { get; set; }

        public DateTime? unsubdate { get; set; }
    }
}
