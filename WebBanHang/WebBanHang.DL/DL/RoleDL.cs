using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.ServiceCollection;
using WebBanHang.DL.BaseDL;

namespace WebBanHang.DL.DL
{
    public class RoleDL : BaseDL<Role>, IRoleDL
    {
        public RoleDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        { 
        }

        /// <summary>
        /// Thêm mới vai trò
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Role? insertRoleCustom(Role role, List<RoleModuleCustom> listRoleModule)
        {
            var result = Insert(role);
            if(result != null)
            {
                role = (Role)result;
                role = GetEntityByProperty(nameof(role.rolecode), role.rolecode);
                List<RoleModule> listEntity = listRoleModule.Select(x => 
                {
                    var roleModule = new RoleModule();
                    roleModule.idrole = role.idrole;
                    roleModule.idmodule = x.idmodule;
                    roleModule.permission = JsonConvert.SerializeObject(x.permission);
                    return roleModule;
                }).ToList();
                bool isSuccess = _dbHelper.InsertBulk(listEntity);
                return role;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Update vai trò
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Role? updateRoleCustom(Role role, List<RoleModuleCustom> listRoleModule)
        {
            var result = Update(role, role.idrole);
            List<RoleModule> listEntity = listRoleModule.Select(x =>
            {
                var roleModule = new RoleModule();
                roleModule.idrole = role.idrole;
                roleModule.idmodule = x.idmodule;
                roleModule.idrolemodule = x.idrolemodule;
                roleModule.permission = JsonConvert.SerializeObject(x.permission);
                return roleModule;
            }).ToList();
            bool isSuccess = _dbHelper.UpdateBulk(listEntity);
            if (isSuccess)
            {
                return role;
            }
            return null;
        }

        /// <summary>
        /// Update vai trò
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object getRoleDetail(int roleID)
        {
            Role role = GetEntityById(roleID);
            string sql = "select rm.*, m.layoutname as modulename from rolemodule rm left join module m on rm.idmodule = m.idmodule where rm.idrole = @roleID";
            DynamicParameters param = new DynamicParameters();
            param.Add("@roleID", roleID);
            List<RoleModule> listRoleModule = _dbHelper.Query<RoleModule>(sql, param);
            return new
            {
                RoleInfo = role,
                ListRoleModule = listRoleModule
            };
        }


    }
}
