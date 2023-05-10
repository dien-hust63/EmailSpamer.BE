using Gather.ApplicationCore.Constant;
using Gather.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Constant;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Services;
using WebBanHang.DL.DL;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.BL.BL
{
    public class CampaignBL : BaseBL<Campaign>, ICampaignBL
    {
        ICampaignDL _campaignDL;

        ISenderDL _senderDL;
        IReceiverDL _receiverDL;
        IMailBL _mailBL;
        IConfiguration _configuration;

        public CampaignBL(IBaseDL<Campaign> baseDL, ICampaignDL campaignDL, ISenderDL senderDL, IReceiverDL receiverDL, IMailBL mailBL, IConfiguration configuration) : base(baseDL)
        {
            _campaignDL = campaignDL;
            _senderDL = senderDL;
            _receiverDL = receiverDL;
            _mailBL = mailBL;
            _configuration = configuration;
        }

        /// <summary>
        /// send Email for guest
        /// </summary>
        /// <param name="emailParam"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult sendEmail(EmailCampaignParam emailParam)
        {
            ServiceResult serviceResult = new ServiceResult();
            List<Sender> senderList = _senderDL.GetAllEntities().ToList();
            List<Receiver> receiverList = _receiverDL.GetAllEntities().ToList();
            CommonSetting setting = _campaignDL.getEmailSetting();

            List<SenderDaily> senderDailyList = _senderDL.getSenderToday();
            int maxEmailInDay = setting.maxemail;

            // get all sent email in campaign
            List<CampaignDetail> listCampaignDetail = _campaignDL.GetListCampaignDetail(emailParam.CampaignID);

            List<int> listReceiverNotWait = listCampaignDetail.FindAll(x => x.statusid == (int)EmailCampaignStatus.Sent || x.statusid == (int)EmailCampaignStatus.Unsubcribe).Select(x => x.idreceiver).ToList();

            List<Receiver> listReceiverWait = receiverList.FindAll(x => !listReceiverNotWait.Contains(x.idreceiver));

            Campaign campaign = _campaignDL.GetEntityById(emailParam.CampaignID);
            string path = campaign.filepath;
            string bodyEmail = File.ReadAllText(path);
            // send email and update status
            int countSender = senderList.Count;
            int countReceiver = listReceiverWait.Count;
            for (int i = 0; i < countSender; i++)
            {
                SenderDaily sd = senderDailyList.FirstOrDefault(x => x.senderid == senderList[i].idsender);
                int maxSentRemain = 0;

                if (sd != null)
                {
                    if(sd.sentnumber < maxEmailInDay)
                    {
                        maxSentRemain = maxEmailInDay - sd.sentnumber; 
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    maxSentRemain = maxEmailInDay;
                }
                int startReceiver = i * maxSentRemain;
                if (startReceiver > countReceiver - 1)
                {
                    break;
                }
                int endReceiver = startReceiver + maxSentRemain <= countReceiver - 1 ? startReceiver + maxSentRemain : countReceiver - 1;
                for (int j = startReceiver; j <= endReceiver; j++)
                {
                    ServiceResult result = new ServiceResult();
                    result = sendEmailCampaign(emailParam, senderList[i], listReceiverWait[j], bodyEmail, campaign.subjectemail);

                    if (result.Success)
                    {
                        CampaignDetail campaignDetail = listCampaignDetail.Find(x => x.idreceiver == listReceiverWait[j].idreceiver);
                        if (campaignDetail != null)
                        {
                            campaignDetail.statusid = (int)EmailCampaignStatus.Sent;
                            campaignDetail.statusname = EmailCampaignStatus.Sent.GetDisplayName();
                            _campaignDL.updateCampaignDetail(campaignDetail, senderList[i].idsender);
                        }
                        else
                        {
                            CampaignDetail campaignDetailNew = new CampaignDetail()
                            {
                                idcampaign = emailParam.CampaignID,
                                statusid = (int)EmailCampaignStatus.Sent,
                                statusname = EmailCampaignStatus.Sent.GetDisplayName(),
                                idreceiver = listReceiverWait[j].idreceiver,
                                receiver = listReceiverWait[j].email
                            };
                            _campaignDL.insertCampaignDetail(campaignDetailNew, senderList[i].idsender);
                        }
                    }
                }
            }
            return serviceResult;

        }

        public ServiceResult sendEmailCampaign(EmailCampaignParam param, Sender sender, Receiver receiver, string bodyEmail, string subjectemail)
        {

            MailRequest mailContent = new MailRequest();
            mailContent.ToEmail = receiver.email;
            mailContent.Subject = subjectemail;
            int fakeReceiverID = receiver.idreceiver + int.Parse(_configuration["FakeID"].ToString());
            int fakeCampaignID = param.CampaignID + int.Parse(_configuration["FakeID"].ToString());
            string linkUnsubcribe = string.Format(_configuration["UnsubcribeURL"].ToString(), fakeReceiverID, fakeCampaignID);
            mailContent.Body = String.Format(bodyEmail, linkUnsubcribe);
            mailContent.FromEmail = sender.email;
            mailContent.FromEmailPassWord = sender.password;
            mailContent.FromDisplayName = sender.displayname;
            return _mailBL.sendEmail(mailContent);
        }

        public void Unsubcribe(int id, int campaignID)
        {
            int fakeID = int.Parse(_configuration["FakeID"].ToString());
            if (id > fakeID && campaignID > fakeID)
            {
                CampaignDetail campaignDetail = new CampaignDetail()
                {
                    idcampaign = campaignID - fakeID,
                    idreceiver = id - fakeID,
                    statusid = (int)EmailCampaignStatus.Unsubcribe,
                    statusname = EmailCampaignStatus.Unsubcribe.GetDisplayName()
                };
                _campaignDL.unSubcribe(campaignDetail);
            }
        }

        /// <summary>
        /// Thêm mới campaign
        /// </summary>
        /// <param name="id"></param>
        /// <param name="campaignID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult addNewCampaign(CampaignParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (param.file != null)
            {
                var fileextension = Path.GetExtension(param.file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "emailcontent", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    param.file.CopyTo(fs);
                }
                serviceResult.Data = _campaignDL.addNewCampaign(param, filepath);
            }
            else
            {
                serviceResult.setError("Chưa truyền file nội dung email.");
            }


            return serviceResult;
        }

        /// <summary>
        /// lấy chi tiết campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult getDetailCustom(int id)
        {
            ServiceResult serviceResult = new ServiceResult();
            Campaign campaign = _campaignDL.GetEntityById(id);

            List<Receiver> receiverList = _receiverDL.GetAllEntities().ToList();

            // get all sent email in campaign
            List<CampaignDetail> listCampaignDetail = _campaignDL.GetListCampaignDetail(id);

            campaign.sentemail = listCampaignDetail.Count(x => x.statusid == (int)EmailCampaignStatus.Sent);
            campaign.unsubcribe = listCampaignDetail.Count(x => x.statusid == (int)EmailCampaignStatus.Unsubcribe);
            campaign.waitemail = receiverList.Count - campaign.sentemail - campaign.unsubcribe;
            campaign.total = receiverList.Count;
            serviceResult.Data = campaign;
            return serviceResult;
        }

        public ServiceResult updateEmailSetting(EmailSettingParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            bool result =  _campaignDL.updateEmailSetting(param.emailmax);
            serviceResult.Data = result;
            serviceResult.Success = result;
            return serviceResult;
        }

        public ServiceResult getEmailSetting()
        {
            ServiceResult serviceResult = new ServiceResult();
            CommonSetting cs = _campaignDL.getEmailSetting();
            if (cs != null)
            {
                serviceResult.Data = cs.maxemail;
                return serviceResult;
            }
            serviceResult.setError("Chưa thiết lập mặc định");
            return serviceResult;
        }
    }
}
