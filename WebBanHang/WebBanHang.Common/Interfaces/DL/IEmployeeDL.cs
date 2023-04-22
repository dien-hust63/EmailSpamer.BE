using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        bool activeAccount(Employee employee);

        bool deactiveAccount(Employee employee);

        /// <summary>
        /// Lấy thông tin nhân viên theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Employee GetEmployeeInfoByEmail(string email);
    }
}
