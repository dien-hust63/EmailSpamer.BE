using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class CampaignParam
    {
        public string campaigncode{ get; set; }

        public string campaignname { get; set; }

        public DateTime? startdate { get; set; }

        public DateTime? enddate { get; set; }

        public string subjectemail { get; set; }

        public IFormFile file { get; set; }

    }
}
