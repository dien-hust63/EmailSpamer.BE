using Gather.ApplicationCore.Constant;
using Gather.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly static int MAX_EMAIL_BATCH = 100;

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

            // get all sent email in campaign
            List<CampaignDetail> listCampaignDetail = _campaignDL.GetListCampaignDetail(emailParam.CampaignID);

            List<int> listReceiverWaitID = listCampaignDetail.FindAll(x => x.statusid == (int)EmailCampaignStatus.Sent || x.statusid == (int)EmailCampaignStatus.Unsubcribe).Select(x => x.idreceiver).ToList();

            List<Receiver> listReceiverWait = receiverList.FindAll(x => !listReceiverWaitID.Contains(x.idreceiver));

            string path = @"emailcontent\emailcontent.txt";
            string bodyEmail = File.ReadAllText(path);
            // send email and update status
            int countSender = senderList.Count;
            int countReceiver = listReceiverWait.Count;
            for(int i = 0; i < countSender; i++)
            {
                int startReceiver = i * MAX_EMAIL_BATCH;
                if(startReceiver > countReceiver - 1)
                {
                    break;
                }
                int endReceiver = startReceiver +  MAX_EMAIL_BATCH <= countReceiver - 1 ? startReceiver + MAX_EMAIL_BATCH : countReceiver - 1;
                for (int j = startReceiver; j <= endReceiver; j++)
                {
                    ServiceResult result = sendEmailCampaign(emailParam, senderList[i], listReceiverWait[j], bodyEmail);

                    if (result.Success)
                    {
                        CampaignDetail campaignDetail = listCampaignDetail.Find(x => x.idreceiver == listReceiverWait[j].idreceiver);
                        if(campaignDetail != null)
                        {
                            campaignDetail.statusid = (int)EmailCampaignStatus.Sent;
                            campaignDetail.statusname = EmailCampaignStatus.Sent.GetDisplayName();
                            _campaignDL.updateCampaignDetail(campaignDetail);
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
                            _campaignDL.insertCampaignDetail(campaignDetailNew);
                        }
                    }
                }
            }
            return serviceResult;
            
        }

        public ServiceResult sendEmailCampaign(EmailCampaignParam param, Sender sender, Receiver receiver, string bodyEmail)
        {

            MailRequest mailContent = new MailRequest();
            mailContent.ToEmail = receiver.email;
            mailContent.Subject = "SoICT 2022: The 11th International Symposium on Information and Communication Technology";
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
    }
}
