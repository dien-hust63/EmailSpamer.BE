using Gather.ApplicationCore.Entities;
using Gather.ApplicationCore.Entities.Param;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;

namespace WebBanHang.Api.Controllers
{
    public class BranchController : BaseEntityController<Branch>
    {
        IBaseBL<Branch> _baseBL;
        IBranchBL _branchBL;
        public BranchController(IBaseBL<Branch> baseBL, IBranchBL branchBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _branchBL = branchBL;
        }

        /// <summary>
        /// lấy danh sách chi nhánh của người dùng
        /// </summary>
        /// <param name="entity">Dữ liệu được thêm</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpPost("getBrancByUser")]
        public async Task<ServiceResult> getBrancByUser(Employee employee)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult.Data = _branchBL.getBrancByUser(employee.email);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;

        }
    }
}
