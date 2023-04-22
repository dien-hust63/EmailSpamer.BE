using Gather.ApplicationCore.Entities;
using Gather.ApplicationCore.Entities.Param;
using Microsoft.AspNetCore.Authorization;
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

namespace WebBanHang.Api.Controllers
{
    public class ModuleController : BaseEntityController<Module>
    {
        IBaseBL<Module> _baseBL;
        IModuleBL _moduleBL;
        public ModuleController(IBaseBL<Module> baseBL, IModuleBL moduleBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _moduleBL = moduleBL;
        }


        [HttpPost("getModulePermission")]
        public ServiceResult getModulePermission()
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                return serviceResult;
            }
            catch (Exception ex)

            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpPost("getModulePermissionDefault")]
        public ServiceResult getModulePermissionDefault()
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _moduleBL.getModulePermissionDefault();
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
