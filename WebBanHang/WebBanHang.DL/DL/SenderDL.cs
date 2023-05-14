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
using static Dapper.SqlMapper;
using static Gather.ApplicationCore.Constant.RoleProject;

namespace WebBanHang.DL.DL
{
    public class SenderDL : BaseDL<Sender>, ISenderDL   
    {
        public SenderDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        { 
        }

        public List<SenderDaily> getSenderToday()
        {
            string storeName = "Proc_GetSenderToday";
            DynamicParameters dynamicParam = new DynamicParameters();
            return _dbHelper.Query<SenderDaily>(storeName, dynamicParam, commandType: CommandType.StoredProcedure);
        }

        public BasePagingResponse<Sender> getSenderPaging(BasePagingParam param)
        {
            BasePagingResponse<Sender> basePagingResponse = new BasePagingResponse<Sender>();
            string storeName = "Proc_GetSenderPaging";
            string textSearch = "";
            if(param.ListFilter.Count > 0)
            {
                textSearch = param.ListFilter.FirstOrDefault().FilterValue;
            }
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("v_Take", param.PageSize);
            dynamicParam.Add("v_SKip", param.PageSize * (param.PageIndex - 1));
            dynamicParam.Add("v_Search", textSearch);
            (List<Sender> listResult, List<int> totalPage) = _dbHelper.QueryMultipleResult<Sender,int>(storeName, dynamicParam, commandType: CommandType.StoredProcedure);
            basePagingResponse.listPaging = listResult;
            basePagingResponse.Total = totalPage.FirstOrDefault();
            return basePagingResponse;
        }
    }
}
