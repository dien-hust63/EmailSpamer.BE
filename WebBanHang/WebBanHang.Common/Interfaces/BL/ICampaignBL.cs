using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.BL
{
    public interface ICampaignBL : IBaseBL<Campaign>
    {
        public ServiceResult sendEmail(EmailCampaignParam emailParam);

        public void Unsubcribe(int id, int campaignID);

        public ServiceResult addNewCampaign(CampaignParam param);

        public ServiceResult getDetailCustom(int id);
    }
}
