using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Services;

namespace WebBanHang.BL.BL
{
    public class ModuleBL : BaseBL<Module>, IModuleBL
    {
        IModuleDL _moduleDL;

        public ModuleBL(IBaseDL<Module> baseDL, IModuleDL moduleDL) : base(baseDL)
        {
            _moduleDL = moduleDL;
        }

        /// <summary>
        /// Lấy danh sách module với các quyền mặc định
        /// </summary>
        /// <returns></returns>
        public ServiceResult getModulePermissionDefault()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _moduleDL.getModulePermissionDefault();
            return serviceResult;
        }
    }
}
