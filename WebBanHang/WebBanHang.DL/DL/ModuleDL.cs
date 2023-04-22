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

namespace WebBanHang.DL.DL
{
    public class ModuleDL : BaseDL<Module>, IModuleDL
    {
        public ModuleDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        { 
        }
        /// <summary>
        /// Lấy danh sách module với các quyền mặc định
        /// </summary>
        /// <returns></returns>
        public List<ModulePermissionDefault> getModulePermissionDefault()
        {
            string store = "Proc_GetModulePermissionDefault";
            DynamicParameters dynamicParam = new DynamicParameters();
            return _dbHelper.Query<ModulePermissionDefault>(store, dynamicParam, commandType: CommandType.StoredProcedure);
        }
    }
}
