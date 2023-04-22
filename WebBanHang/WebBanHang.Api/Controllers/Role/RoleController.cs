using Gather.ApplicationCore.Entities;
using Gather.ApplicationCore.Entities.Param;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;

namespace WebBanHang.Api.Controllers
{
    public class RoleController : BaseEntityController<Role>
    {
        IBaseBL<Role> _baseBL;
        IRoleBL _roleBL;
        public RoleController(IBaseBL<Role> baseBL, IRoleBL roleBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _roleBL = roleBL;
        }


        [HttpPost("insertRole")]
        public ServiceResult insertRoleCustom([FromBody] Dictionary<string, object> param)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _roleBL.insertRoleCustom(param);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }


        [HttpPost("updateRole")]
        public ServiceResult updateRoleCustom([FromBody] Dictionary<string, object> param)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _roleBL.updateRoleCustom(param);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpGet("getDetail/{entityId}")]
        public ServiceResult getRoleDetail(int entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _roleBL.getRoleDetail(entityId);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }


    }
}
