using Dapper;
using Gather.ApplicationCore.Constant;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.ServiceCollection;
using WebBanHang.DL.BaseDL;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.DL.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        public EmployeeDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        {
        }

        /// <summary>
        /// chuyển trạng thái tài khoản
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool activeAccount(Employee employee)
        {
            string sql = "update employee e set e.statusid = @statusid, e.statusname = @statustext, e.password = @password where e.idemployee = @idemployee";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@statusid", (int)AccountStatus.Active);
            dynamicParam.Add("@statustext", AccountStatus.Active.GetDisplayName());
            dynamicParam.Add("@password", employee.password);
            dynamicParam.Add("@idemployee", employee.idemployee);
            return _dbHelper.Execute(sql, dynamicParam, commandType: CommandType.Text) > 0;
        }


        /// <summary>
        /// chuyển trạng thái tài khoản
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool deactiveAccount(Employee employee)
        {
            string sql = "update employee e set e.statusid = @statusid, e.statusname = @statustext where e.idemployee = @idemployee";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@statusid", (int)AccountStatus.NotActive);
            dynamicParam.Add("@statustext", AccountStatus.NotActive.GetDisplayName());
            dynamicParam.Add("@idemployee", employee.idemployee);
            return _dbHelper.Execute(sql, dynamicParam, commandType: CommandType.Text) > 0;
        }

        /// <summary>
        /// Lấy thoogn tin nhân viên theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee GetEmployeeInfoByEmail(string email)
        {
            string sql = "select * from employee e where e.email = @email limit 1";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@email", email);
            return _dbHelper.QueryFirstOrDefault<Employee>(sql, dynamicParam, commandType: CommandType.Text);
        }
    }
}
