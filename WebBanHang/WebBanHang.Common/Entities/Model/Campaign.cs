using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class Campaign
    {
        [AttributeCustomId]
        public int idcampaign { get; set; }

        public string campaigncode{ get; set; }

        public string campaignname { get; set; }

        public DateTime? startdate { get; set; }

        public DateTime? enddate { get; set; }

        [AttributeCustomNotMap]
        public string startdatetext
        {
            get
            {
                return startdate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }

        [AttributeCustomNotMap]
        public string enddatetext
        {
            get
            {
                return enddate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
    }
}
