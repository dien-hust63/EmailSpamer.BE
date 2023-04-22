using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;

namespace WebBanHang.Common.Interfaces.BL
{
    public interface IAuthenticationBL
    {
        public ServiceResult registerManagementApplication(Employee employee);

        public ServiceResult loginManagementApplication(Employee employee);

        public bool checkUserAccount(LoginParam employee);

        /// <summary>
        /// Lấy danh sách các quyền của role đối với module
        /// </summary>
        /// <returns></returns>
        List<RoleModule> GetListRoleModuleByUser(string email);

        UserInfo GetUserInfo(string email, bool isemployee = true);
    }
}
