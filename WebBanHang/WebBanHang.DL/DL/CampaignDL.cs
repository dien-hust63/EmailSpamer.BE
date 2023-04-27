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

        public List<CampaignDetail> GetListCampaignDetail(int campaignID)
        {
            string sql = "select cd.* from campaigndetail cd where cd.idcampaign = @CampaignID";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@CampaignID", campaignID);
            return _dbHelper.Query<CampaignDetail>(sql, dynamicParam, commandType: CommandType.Text);
        }

        public bool insertCampaignDetail(CampaignDetail campaignDetail)
        {
            string sql = "INSERT INTO campaigndetail(idcampaign,receiver,idreceiver,statusid,statusname,senddate) values (@CampaignID, @Receiver, @ReceiverID, @StatusID, @StatusName, @SendDate)";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@CampaignID", campaignDetail.idcampaign);
            dynamicParam.Add("@StatusID", campaignDetail.statusid);
            dynamicParam.Add("@StatusName", campaignDetail.statusname);
            dynamicParam.Add("@ReceiverID", campaignDetail.idreceiver);
            dynamicParam.Add("@Receiver", campaignDetail.receiver);
            dynamicParam.Add("@SendDate", DateTime.Now);
            return _dbHelper.Execute(sql, dynamicParam, commandType: CommandType.Text) > 0;
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
        public bool updateCampaignDetail(CampaignDetail campaignDetail)
        {
            string sql = "update campaigndetail cd set cd.statusid = @StatusID, cd.statusname = @StatusName, cd.senddate = @SendDate where cd.idcampaigndetail = @CampaignDetailID";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@CampaignDetailID", campaignDetail.idcampaigndetail);
            dynamicParam.Add("@StatusID", campaignDetail.statusid);
            dynamicParam.Add("@StatusName", campaignDetail.statusname);
            dynamicParam.Add("@SendDate", DateTime.Now);
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
