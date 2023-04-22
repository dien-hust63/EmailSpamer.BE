using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.DL
{
    public interface IRoleDL : IBaseDL<Role>
    {
        public Role? insertRoleCustom(Role role, List<RoleModuleCustom> listRoleModule);

        public Role? updateRoleCustom(Role role, List<RoleModuleCustom> listRoleModule);

        object getRoleDetail(int entityId);
    }
}
