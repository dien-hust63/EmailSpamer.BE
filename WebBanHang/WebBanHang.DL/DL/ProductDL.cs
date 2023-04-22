using Dapper;
using Gather.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Library;
using WebBanHang.Common.ServiceCollection;
using WebBanHang.DL.BaseDL;
using static Dapper.SqlMapper;
using static Gather.ApplicationCore.Constant.RoleProject;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.DL.DL
{
    public class ProductDL : BaseDL<Product>, IProductDL
    {
        public ProductDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        { 
        }

        /// <summary>
        /// Thêm mới hàng hóa
        /// </summary>
        /// <param name="product"></param>
        /// <param name="listProductDetail"></param>
        /// <returns></returns>
        public Product InsertProductDetail(List<Product> listProduct, List<ProductDetail> listProductDetail, List<Branch> listBranch, int currentBranch)
        {
            var result = _dbHelper.InsertBulk(listProduct);
            if (result)
            {
                List<ProductDetail> listProductDetailAdd = new List<ProductDetail>();
                
                Product firstProduct = listProduct.First();
                List<Product> listProductAfterSave = GetListEntityByProperty(nameof(firstProduct.productcode), firstProduct.productcode);
                
                Product curProduct = listProductAfterSave.Find(x => x.branchid == currentBranch);
                foreach (var item in listProductDetail)
                {
                    item.idproduct = curProduct.idproduct;
                }
                listProductDetailAdd.AddRange(listProductDetail);
                foreach (var product in listProductAfterSave)
                {
                    if(product.idproduct != curProduct.idproduct)
                    {
                        List<ProductDetail> listProductDetailClone = CommonFunction.Clone<List<ProductDetail>>(listProductDetail) ;
                        foreach (var item in listProductDetailClone)
                        {
                            item.inventory = 0;
                            item.idproduct = product.idproduct;
                        }
                        listProductDetailAdd.AddRange(listProductDetailClone);
                    }
                }
                bool isSuccess = _dbHelper.InsertBulk(listProductDetailAdd);
                return curProduct;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sửa hàng hóa
        /// </summary>
        /// <param name="product"></param>
        /// <param name="listProductDetail"></param>
        /// <returns></returns>
        public Product UpdateProductDetail(Product product, List<ProductDetail> listProductDetail)
        {
            var result = Update(product, product.idproduct);
            bool isSuccess = _dbHelper.UpdateBulk(listProductDetail);
            if (isSuccess)
            {
                return product;
            }
            return null;
        }

        /// <summary>
        /// Luwu linh anh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imageLink"></param>
        /// <returns></returns>
        public bool saveImageLinkProduct(string productcode, string imageLink)
        {
            string sql = "update product set image = @imagelink where productcode = @productcode";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@imagelink", imageLink);
            dynamicParam.Add("@productcode", productcode);
            return _dbHelper.QueryFirstOrDefault<int>(sql, dynamicParam, System.Data.CommandType.Text) > 0;
        }

        /// <summary>
        /// Update vai trò
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object getProductDetail(int productID)
        {
            Product product = GetEntityById(productID);
            string sql = "select pd.* from productdetail pd inner join product p on pd.idproduct  = p.idproduct and pd.idproduct = @idproduct";
            DynamicParameters param = new DynamicParameters();
            param.Add("@idproduct", productID);
            List<ProductDetail> listProductDetail = _dbHelper.Query<ProductDetail>(sql, param);
            return new
            {
                Product = product,
                ProductDetail = listProductDetail
            };
        }

        /// <summary>
        /// Lấy danh sách sản phẩm 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BasePagingResponse<Product> getListProductByCategory(ProductByCategoryParam param)
        {
            BasePagingResponse<Product> basePagingResponse = new BasePagingResponse<Product>();
            string sql = "select p.* from product p inner join branch b on p.branchid = b.idbranch and  b.isaddressdefault = true ";
            string v_where = "where (@categoryid is null or p.categoryid = @categoryid) and (@searchtext is null or p.productname like concat('%', @searchtext, '%') or p.categoryname like concat('%', @searchtext, '%')) ";
            string v_orderBy = "";
            switch (param.SortBy)
            {
                case (int)ProductSortOrder.Newest:
                    v_orderBy = "Order by p.createddate desc, p.productname asc ";
                    break;

                case (int)ProductSortOrder.PriceDes:
                    v_orderBy = "Order by p.sellprice desc, p.productname asc ";
                    break;
                case (int)ProductSortOrder.PriceAsc:
                    v_orderBy = "Order by p.sellprice asc, p.productname asc ";
                    break;
                default:
                    v_orderBy = "Order by p.createddate desc, p.productname asc ";
                    break;
            }
            
            string v_paging = "LIMIT @take OFFSET @skip; ";
            sql += string.Concat(v_where, v_orderBy, v_paging);

            string sqlCount = "select count(p.idproduct) from product p inner join branch b on p.branchid = b.idbranch and  b.isaddressdefault = true ";
            sql += string.Concat(sqlCount, v_where, v_orderBy);
            DynamicParameters dicParam = new DynamicParameters();
            dicParam.Add("@categoryid", param.categoryid);
            dicParam.Add("@searchtext", param.searchtext == "" ? null: param.searchtext);
            dicParam.Add("@take", param.PageSize);
            dicParam.Add("@skip", (param.PageIndex - 1) * param.PageSize);
            (List<Product> listResult, List<int> totalPage) = _dbHelper.QueryMultipleResult<Product, int>(sql, dicParam, System.Data.CommandType.Text);
            basePagingResponse.listPaging = listResult;
            basePagingResponse.Total = totalPage.FirstOrDefault();
            return basePagingResponse;
        }

        public List<ProductDetail> getProductDetailByBranch(ProductPopupParam param)
        {
            string sql = "select pd.* from productdetail pd left join product p on pd.idproduct  = p.idproduct WHERE p.branchid = @branchid AND (p.productname like CONCAT('%',@searchtext,'%') OR p.productcode like CONCAT('%',@searchtext,'%') OR @searchtext = '') ";
            DynamicParameters dicParam = new DynamicParameters();
            dicParam.Add("@branchid", param.branchid);
            dicParam.Add("@searchtext", param.searchtext);
            List<ProductDetail> listProductDetail = _dbHelper.Query<ProductDetail>(sql, dicParam);
            return listProductDetail;
        }
    }
}
