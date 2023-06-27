using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Constant;

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

        [HttpPost("testSendEmail")]
        public ServiceResult testSendEmail(TestSendEmailParam emailParam)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _campaignBL.testSendEmail(emailParam);
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }



        [HttpPost("resetCampaign")]
        public ServiceResult resetCampaign(CampaignIDParam param)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _campaignBL.resetCampaign(param.idcampaign);
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
        public ServiceResult addNewCampaign([FromForm] CampaignParam campaignParam)
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

        [HttpPost("updateCampaign")]
        public ServiceResult updateCampaign([FromForm] CampaignUpdateParam campaignParam)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _campaignBL.updateCampaign(campaignParam);
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

        [HttpPost("updateEmailSetting")]
        public ServiceResult updateEmailSetting(EmailSettingParam param)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult.Data = _campaignBL.updateEmailSetting(param);
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpGet("getEmailSetting")]
        public ServiceResult getEmailSetting()
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult.Data = _campaignBL.getEmailSetting();
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
