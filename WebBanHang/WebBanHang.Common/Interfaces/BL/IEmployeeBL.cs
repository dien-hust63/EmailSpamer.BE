using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        ServiceResult activeAccount(Employee employee);

        ServiceResult deactiveAccount(Employee employee);

        /// <summary>
        /// Lấy thông tin nhân viên theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Employee GetEmployeeInfoByEmail(string email);
    }
}
