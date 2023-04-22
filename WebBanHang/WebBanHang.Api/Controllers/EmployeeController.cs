
using Gather.ApplicationCore.Entities;
using Gather.ApplicationCore.Entities.Param;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Api.Services;
using WebBanHang.Common.Entities;
using WebBanHang.Common;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using Microsoft.AspNetCore.Authorization;

namespace WebBanHang.Api.Controllers
{
    public class EmployeeController : BaseEntityController<Employee>
    {
        IBaseBL<Employee> _baseBL;
        IEmployeeBL _employeeBL;
        public EmployeeController(IBaseBL<Employee> baseBL, IEmployeeBL employeeBL) :base(baseBL)
        {
            _baseBL = baseBL;
            _employeeBL = employeeBL;
        }

        [HttpPost("active")]
        public ServiceResult activeAccount(Employee employee)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _employeeBL.activeAccount(employee);
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpPost("deactive")]
        public ServiceResult deactiveAccount(Employee employee)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _employeeBL.deactiveAccount(employee);
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }


    }
}
