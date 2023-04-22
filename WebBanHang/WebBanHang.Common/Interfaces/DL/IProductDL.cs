using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.DL
{
    public interface IProductDL : IBaseDL<Product>
    {
        public bool saveImageLinkProduct(string productcode, string imageLink);

        /// <summary>
        /// Thêm mới product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="listProductDetail"></param>
        /// <returns></returns>
        public Product InsertProductDetail(List<Product> product, List<ProductDetail> listProductDetail, List<Branch> listBranch, int branch);

        /// <summary>
        /// Xem danh sách sản phẩm hàng hóa theo chi nhánh
        /// </summary>
        /// <param name="branchid"></param>
        /// <returns></returns>

        public List<ProductDetail> getProductDetailByBranch(ProductPopupParam param);

        /// <summary>
        /// Lấy chi tiết sản phẩm
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        object getProductDetail(int entityId);

        /// <summary>
        /// Sửa product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="listProductDetail"></param>
        /// <returns></returns>
        public Product UpdateProductDetail(Product product, List<ProductDetail> listProductDetail);

        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BasePagingResponse<Product> getListProductByCategory(ProductByCategoryParam param);
    }
}
