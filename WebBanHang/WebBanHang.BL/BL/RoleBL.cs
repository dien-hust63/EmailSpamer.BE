using Gather.ApplicationCore.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Services;

namespace WebBanHang.BL.BL
{
    public class RoleBL : BaseBL<Role>, IRoleBL
    {
        IRoleDL _roleDL;

        public RoleBL(IBaseDL<Role> baseDL, IRoleDL roleDL) : base(baseDL)
        {
            _roleDL = roleDL;
        }

        /// <summary>
        /// Lấy chi tiết vai trò
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult getRoleDetail(int entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _roleDL.getRoleDetail(entityId);
            return serviceResult;
        }

        /// <summary>
        /// Thêm mới quyền
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult insertRoleCustom(Dictionary<string, object> param)
        {
            ServiceResult serviceResult = new ServiceResult();
            Role role = new Role();
            List<RoleModuleCustom> listRoleModule = new List<RoleModuleCustom>();
            object value;
            if (param.TryGetValue("RoleInfo", out value))
            {
                role = JsonSerializer.Deserialize<Role>(value.ToString());
            }
            if (param.TryGetValue("ListRoleModule", out value))
            {
                listRoleModule = JsonSerializer.Deserialize<List<RoleModuleCustom>>(value.ToString());
            }
            Role? result = _roleDL.insertRoleCustom(role, listRoleModule);
            serviceResult.Data = result;
            return serviceResult;
        }


        /// <summary>
        /// update role
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ServiceResult updateRoleCustom(Dictionary<string, object> param)
        {
            ServiceResult serviceResult = new ServiceResult();
            Role role = new Role();
            List<RoleModuleCustom> listRoleModule = new List<RoleModuleCustom>();
            object value;
            if (param.TryGetValue("RoleInfo", out value))
            {
                role = JsonSerializer.Deserialize<Role>(value.ToString());
            }
            if (param.TryGetValue("ListRoleModule", out value))
            {
                listRoleModule = JsonSerializer.Deserialize<List<RoleModuleCustom>>(value.ToString());
            }
            Role? result = _roleDL.updateRoleCustom(role, listRoleModule);
            serviceResult.Data = result;
            return serviceResult;
        }
    }
}
