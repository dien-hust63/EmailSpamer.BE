using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
    public class BranchDL : BaseDL<Branch>, IBranchDL
    {
        public BranchDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        { 
        }

        /// <summary>
        /// lấy dah sách chi nhánh theo user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Branch> getBrancByUser(string email)
        {
            string storeName = "Proc_GetBranchByUser";
            DynamicParameters param = new DynamicParameters();
            param.Add("@email", email);
            List<Branch> listBranch = _dbHelper.Query<Branch>(storeName, param, System.Data.CommandType.StoredProcedure);
            return listBranch;
        }
    }
}
