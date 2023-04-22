using Azure.Storage.Blobs;
using Gather.ApplicationCore.Entities;
using Gather.ApplicationCore.Entities.Param;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.AzureStorage;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;

namespace WebBanHang.Api.Controllers
{
    public class ProductController : BaseEntityController<Product>
    {
        IBaseBL<Product> _baseBL;
        IProductBL _productBL;
        IAzureStorageBL _azureStorageBL;
        public ProductController(IBaseBL<Product> baseBL, IProductBL productBL, IAzureStorageBL azureStorageBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _productBL = productBL;
            _azureStorageBL = azureStorageBL;
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Dữ liệu được thêm</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpPost("insertProduct")]
        public Task<ServiceResult> Insert([FromForm] Product product)
        {
            //Trả về kết quả cho client
            return _productBL.InsertProduct(product);

        }

        [HttpPost("uploadImage")]
        public async Task<ServiceResult> UploadImage([FromForm] List<IFormFile> listImage)
        {
            ServiceResult serviceResult = new ServiceResult();
            //serviceResult = await _productBL.InsertProduct(product);
            return serviceResult;
        }

        /// <summary>
        /// Thêm mới dữ liệu hàng hóa
        /// </summary>
        /// <param name="entity">Dữ liệu được thêm</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpPost("insertProductDetail")]
        public async Task<ServiceResult> InsertProductDetail([FromForm] ProductDetailParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _productBL.InsertProductDetail(param);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpGet("getProductDetail/{entityId}")]
        public ServiceResult getProductDetail(int entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _productBL.getProductDetail(entityId);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }


        [HttpPost("getProductDetailByBranch")]
        public ServiceResult getProductDetailByBranch(ProductPopupParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _productBL.getProductDetailByBranch(param);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpPost("updateProductDetail")]
        public async Task<ServiceResult> updateProductDetail([FromForm] ProductDetailParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _productBL.UpdateProductDetail(param);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }

        [HttpPost("getListProductByCategory")]
        public ServiceResult getListProductByCategory(ProductByCategoryParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult =  _productBL.getListProductByCategory(param);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }
    }
}
