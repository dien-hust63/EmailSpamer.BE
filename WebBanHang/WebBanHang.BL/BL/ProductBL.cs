using Gather.ApplicationCore.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebBanHang.Common.AzureStorage;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Library;
using WebBanHang.Common.Services;
using WebBanHang.DL.DL;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.BL.BL
{
    public class ProductBL : BaseBL<Product>, IProductBL
    {
        IProductDL _productDL;
        IAzureStorageBL _azureStorageBL;
        IBranchBL _branchBL;
        public ProductBL(IBaseDL<Product> baseDL, IProductDL productDL, IAzureStorageBL azureStorageBL, IBranchBL branchBL) : base(baseDL)
        {
            _productDL = productDL;
            _azureStorageBL = azureStorageBL;
            _branchBL = branchBL;
        }

        public async Task<ServiceResult> InsertProduct(Product product)
        {
            ServiceResult serviceResult = new ServiceResult();
            //ProductNotFile productnotfile = new ProductNotFile();
            //serviceResult = Insert(product);
            //Product productAfterSave = new Product();
            //if (!serviceResult.Success)
            //{
            //    return serviceResult;
            //}
            //else
            //{
            //    productnotfile.productcode = product.productcode;
            //    productnotfile.productname = product.productname;
            //}
            //ServiceResult serviceResult2 = await _azureStorageBL.UploadBlobFileAsync(product.mainImage);
            //if (serviceResult2.Success && serviceResult2 != null)
            //{
            //    productnotfile.image  = serviceResult2.Data?.ToString();
            //    // save image link to database
            //    _productDL.saveImageLinkProduct(product.productcode, productnotfile.image);
            //}
            //serviceResult.Data = productnotfile;
            return serviceResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertProductDetail(ProductDetailParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            Product product = new Product();
            List<ProductDetail> listProductDetail = new List<ProductDetail>();
            List<Branch> listBranch = (List<Branch>)_branchBL.GetAllEntities().Data;
            List<Product> listProductWithBranch = new List<Product>();
            
            product = JsonSerializer.Deserialize<Product>(param.product);
            listProductWithBranch.Add(product);
            listProductDetail = JsonSerializer.Deserialize<List<ProductDetail>>(param.productdetail);

            foreach (Branch branch in listBranch)
            {
                if(product.branchid != branch.idbranch)
                {
                    Product newProduct = CommonFunction.Clone<Product>(product);
                    newProduct.inventory = 0;
                    newProduct.branchid = branch.idbranch;
                    newProduct.branchname = branch.branchname;
                    listProductWithBranch.Add(newProduct);
                }
            }
            Product? result = _productDL.InsertProductDetail(listProductWithBranch, listProductDetail, listBranch, product.branchid);
            try
            {
                foreach (var file in param.file)
                {
                    await _azureStorageBL.UploadBlobFileAsync(file);
                }
            }
            catch (Exception ex)
            {

                serviceResult.setError("Upload ảnh lỗi");
                return serviceResult;
            }
            
            serviceResult.Data = result;
            return serviceResult;
        }


        /// <summary>
        /// sửa hàng hóa
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateProductDetail(ProductDetailParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            Product product = new Product();
            List<ProductDetail> listProductDetail = new List<ProductDetail>();
            product = JsonSerializer.Deserialize<Product>(param.product);
            listProductDetail = JsonSerializer.Deserialize<List<ProductDetail>>(param.productdetail);
            Product? result = _productDL.UpdateProductDetail(product, listProductDetail);
            try
            {
                if(param.file != null)
                {
                    foreach (var file in param.file)
                    {
                        await _azureStorageBL.UploadBlobFileAsync(file);
                    }
                }
            }
            catch (Exception ex)
            {

                serviceResult.setError("Upload ảnh lỗi");
                return serviceResult;
            }

            serviceResult.Data = result;
            return serviceResult;
        }

        /// <summary>
        /// Lấy chi tiết sản phầm
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult getProductDetail(int entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _productDL.getProductDetail(entityId);
            return serviceResult;
        }

        /// <summary>
        /// Lấy chi tiết sản phầm
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult getProductDetailByBranch(ProductPopupParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _productDL.getProductDetailByBranch(param);
            return serviceResult;
        }

        /// <summary>
        /// Xử lý sau khi insert
        /// </summary>
        /// <param name="entity"></param>
        protected override async void AfterSave (EntityState entityState, Product entity)
        {
            //if (entityState == EntityState.Add)
            //{
            //    await _azureStorageBL.UploadBlobFileAsync(entity.mainImage);
            //}
        }

        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ServiceResult getListProductByCategory(ProductByCategoryParam param)
        {

            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _productDL.getListProductByCategory(param);
            return serviceResult;
        }

    }
}
