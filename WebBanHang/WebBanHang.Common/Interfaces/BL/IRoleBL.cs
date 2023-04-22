using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.BL
{
    public interface IRoleBL : IBaseBL<Role>
    {
        public ServiceResult insertRoleCustom(Dictionary<string, object> param);

        public ServiceResult updateRoleCustom(Dictionary<string, object> param);

        ServiceResult getRoleDetail(int entityId);
    }
}
