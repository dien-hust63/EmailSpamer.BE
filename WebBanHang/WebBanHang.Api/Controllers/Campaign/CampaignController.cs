using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;

using Gather.ApplicationCore.Entities;
using Gather.ApplicationCore.Entities.Param;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Entities;

namespace WebBanHang.Api.Controllers
{
    public class CampaignController : BaseEntityController<Campaign>
    {
        IBaseBL<Campaign> _baseBL;
        ICampaignBL _campaignBL;
        public CampaignController(IBaseBL<Campaign> baseBL, ICampaignBL campaignBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _campaignBL = campaignBL;
        }


        [HttpPost("sendEmail")]
        public ServiceResult sendEmail(EmailCampaignParam emailParam)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _campaignBL.sendEmail(emailParam);
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpGet("unsubcribe")]
        public ContentResult Unsubcribe(int id, int cp)
        {
            _campaignBL.Unsubcribe(id, cp);
            return base.Content("<div>We won't send this information for you again</div>", "text/html");
        }


        [HttpPost("addNewCampaign")]
        public ServiceResult addNewCampaign([FromForm]CampaignParam campaignParam)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _campaignBL.addNewCampaign(campaignParam);
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpGet("detail/{id}")]
        public ServiceResult getDetailCustom(int id)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _campaignBL.getDetailCustom(id);
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }
    }
}
