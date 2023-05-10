using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.ServiceCollection;
using WebBanHang.DL.BaseDL;
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
    }
}
