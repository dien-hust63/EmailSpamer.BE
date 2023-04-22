using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.DL;

namespace WebBanHang.DL.DL
{
    public class AuthenticationDL : IAuthenticationDL
    {
        IDBHelper _dbHelper;
        public AuthenticationDL(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public Employee checkAuthen(Employee employee )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee getEmployeeByEmail(string email)
        {
            string storeName = "Proc_GetEmployeeByEmail";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("v_email", email);
            return _dbHelper.QueryFirstOrDefault<Employee>(storeName, dynamicParam, System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy danh sách các quyền module theo user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<RoleModule> GetListRoleModuleByUser(string email)
        {
            string storeName = "Proc_GetListRoleModuleByUser";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("v_email", email);
            return _dbHelper.Query<RoleModule>(storeName, dynamicParam, System.Data.CommandType.StoredProcedure);
        }

        public bool registerManagementApplication(Employee employee)
        {
            string storeName = "Proc_RegisterManagement";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("v_email", employee.email);
            dynamicParam.Add("v_password", employee.password);
            return _dbHelper.QueryFirstOrDefault<int>(storeName, dynamicParam, System.Data.CommandType.StoredProcedure) > 0;
        }
    }
}
