using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;

namespace WebBanHang.Common.Interfaces.DL
{
    public interface IAuthenticationDL
    {
        public bool registerManagementApplication(Employee employee);

        public Employee checkAuthen(Employee employee);

        public Employee getEmployeeByEmail(string email);

        public List<RoleModule> GetListRoleModuleByUser(string email);
    }
}
