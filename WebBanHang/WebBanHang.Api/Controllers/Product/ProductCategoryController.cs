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
    public class ProductCategoryController : BaseEntityController<ProductCategory>
    {
        IBaseBL<ProductCategory> _baseBL;
        public ProductCategoryController(IBaseBL<ProductCategory> baseBL) : base(baseBL)
        {
            _baseBL = baseBL;
        }


    }
}
