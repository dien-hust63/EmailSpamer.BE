using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.DL
{
    public interface ICampaignDL : IBaseDL<Campaign>
    {
        public bool updateEmailStatus(EmailCampaignParam emailParam);

        public List<CampaignDetail> GetListCampaignDetail(int campaignID);

        public bool updateCampaignDetail(CampaignDetail campaignDetail, int senderid);

        public bool insertCampaignDetail(CampaignDetail campaignDetail, int senderid);

        public bool unSubcribe(CampaignDetail campaign);

        public bool addNewCampaign(CampaignParam param, string filepath);

        public bool updateEmailSetting(int maxemail);

        public CommonSetting getEmailSetting();

        public bool updateCampaign(CampaignUpdateParam campaignDetail, string filepath);
        public bool resetCampaign(int resetCampaign);
    }
}
