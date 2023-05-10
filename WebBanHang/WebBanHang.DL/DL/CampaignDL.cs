using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.ServiceCollection;
using WebBanHang.DL.BaseDL;
using static Gather.ApplicationCore.Constant.RoleProject;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.DL.DL
{
    public class CampaignDL : BaseDL<Campaign>, ICampaignDL   
    {
        public CampaignDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        { 
        }

        public bool addNewCampaign(CampaignParam param, string filepath)
        {
            string sql = "insert into campaign(campaigncode,campaignname,startdate,enddate,filepath,subjectemail) values (@CampaignCode, @CampaignName, @StartDate, @EndDate, @FilePath,@SubjectEmail)";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@CampaignCode", param.campaigncode);
            dynamicParam.Add("@CampaignName", param.campaignname);
            dynamicParam.Add("@StartDate", param.startdate);
            dynamicParam.Add("@EndDate", param.enddate);
            dynamicParam.Add("@SubjectEmail", param.subjectemail);
            dynamicParam.Add("@FilePath", filepath);
            return _dbHelper.Execute(sql, dynamicParam, commandType: CommandType.Text) > 0;
        }

        public CommonSetting getEmailSetting()
        {
            string sql = "select cd.* from commonsetting cd where cd.identify = @identify";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@identify", "SETTING1");
            return _dbHelper.QueryFirstOrDefault<CommonSetting>(sql, dynamicParam, commandType: CommandType.Text);
        }

        public List<CampaignDetail> GetListCampaignDetail(int campaignID)
        {
            string sql = "select cd.* from campaigndetail cd where cd.idcampaign = @CampaignID";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@CampaignID", campaignID);
            return _dbHelper.Query<CampaignDetail>(sql, dynamicParam, commandType: CommandType.Text);
        }

        public bool insertCampaignDetail(CampaignDetail campaignDetail, int senderID)
        {
            string storeName = "Proc_UpdateAfterSendEmail";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("v_CampaignDetailID", 0);
            dynamicParam.Add("v_CampaignID", campaignDetail.idcampaign);
            dynamicParam.Add("v_Receiver", campaignDetail.receiver);
            dynamicParam.Add("v_ReceiverID", campaignDetail.idreceiver);
            dynamicParam.Add("v_StatusID", campaignDetail.statusid);
            dynamicParam.Add("v_StatusName", campaignDetail.statusname);
            dynamicParam.Add("v_SendDate", DateTime.Now);
            dynamicParam.Add("v_Mode",(int)EntityState.Add);
            dynamicParam.Add("v_SenderID", senderID);
            return _dbHelper.Execute(storeName, dynamicParam, commandType: CommandType.StoredProcedure) > 0;
        }

        public bool unSubcribe(CampaignDetail campaign)
        {
            string sql = "update campaigndetail cd set cd.statusid = @StatusID, cd.statusname = @StatusName, cd.unsubdate = @UnSubcribeDate where cd.idcampaign = @CampaignID and cd.idreceiver = @ReceiverID";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@CampaignID", campaign.idcampaign);
            dynamicParam.Add("@ReceiverID", campaign.idreceiver);
            dynamicParam.Add("@StatusID", campaign.statusid);
            dynamicParam.Add("@StatusName", campaign.statusname);
            dynamicParam.Add("@UnSubcribeDate", DateTime.Now);
            return _dbHelper.Execute(sql, dynamicParam, commandType: CommandType.Text) > 0;
        }

        /// <summary>
        /// update campaign detail
        /// </summary>
        /// <param name="campaignDetail"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool updateCampaignDetail(CampaignDetail campaignDetail, int senderID)
        {
            string storeName = "Proc_UpdateAfterSendEmail";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("v_CampaignDetailID", campaignDetail.idcampaigndetail);
            dynamicParam.Add("v_SendDate", DateTime.Now);
            dynamicParam.Add("v_CampaignID", campaignDetail.idcampaign);
            dynamicParam.Add("v_Receiver", campaignDetail.receiver);
            dynamicParam.Add("v_ReceiverID", campaignDetail.idreceiver);
            dynamicParam.Add("v_StatusID", campaignDetail.statusid);
            dynamicParam.Add("v_StatusName", campaignDetail.statusname);
            dynamicParam.Add("v_Mode", (int)EntityState.Edit);
            dynamicParam.Add("v_SenderID", senderID);
            return _dbHelper.Execute(storeName, dynamicParam, commandType: CommandType.StoredProcedure) > 0;
        }

        public bool updateEmailSetting(int maxemail)
        {
            string sql = "update commonsetting cd set cd.maxemail = @maxemail where cd.identify = @identify";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@identify", "SETTING1");
            dynamicParam.Add("@maxemail", maxemail);
            return _dbHelper.Execute(sql, dynamicParam, commandType: CommandType.Text) > 0;
        }

        /// <summary>
        /// send Email
        /// </summary>
        /// <param name="emailParam"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool updateEmailStatus(EmailCampaignParam emailParam)
        {
            throw new NotImplementedException();



        }
    }
}
