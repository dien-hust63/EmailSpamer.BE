using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.BL
{
    public interface IProductBL : IBaseBL<Product>
    {
        public Task<ServiceResult> InsertProduct(Product product);

        
        /// <summary>
        /// Xem chi tiết sản phẩm
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>

        ServiceResult getProductDetail(int entityId);


        /// <summary>
        /// Xem danh sách sản phẩm hàng hóa theo chi nhánh
        /// </summary>
        /// <param name="branchid"></param>
        /// <returns></returns>

        ServiceResult getProductDetailByBranch(ProductPopupParam param);

        /// <summary>
        /// Thêm mới product
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<ServiceResult> InsertProductDetail(ProductDetailParam param);

        /// <summary>
        /// Sửa sản phẩm
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<ServiceResult> UpdateProductDetail(ProductDetailParam param);

        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ServiceResult getListProductByCategory(ProductByCategoryParam param);
    }
}
