using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.ServiceCollection;
using WebBanHang.DL.BaseDL;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.DL.DL
{
    public class OrderDL : BaseDL<SaleOrder>, IOrderDL
    {
        public OrderDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        { 
        }

        public SaleOrder GetLastestOrder()
        {
            string sql = "select s.* from saleorder s order by s.idsaleorder desc";
            DynamicParameters param = new DynamicParameters();
            SaleOrder order = _dbHelper.QueryFirstOrDefault<SaleOrder>(sql, param);
            return order;
        }

        /// <summary>
        /// Lấy chi tiêt đơn hàng
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public object getOrderDetail(int entityId)
        {
            SaleOrder order = GetEntityById(entityId);
            string sql = "select pd.* from orderdetail pd inner join saleorder p on pd.idsaleorder  = p.idsaleorder and p.idsaleorder = @idsaleorder";
            DynamicParameters param = new DynamicParameters();
            param.Add("@idsaleorder", entityId);
            List<OrderDetail> listOrderDetail = _dbHelper.Query<OrderDetail>(sql, param);
            return new
            {
                SaleOrder = order,
                OrderDetail = listOrderDetail
            };
        }

        public List<int> getReportRevenueByBranch(TimeParam param)
        {
            string storeName = "select COALESCE(sum(m.totalprice),0) from branch b left join (select case when s.deliverprice is null then s.totalprice else s.totalprice - s.deliverprice end as totalprice,s.branchid,s.branchname from saleorder s where date(s.orderdate) >= date(@startdate) and date(s.orderdate) <= date(@enddate) and s.statusid = 1) as m on b.idbranch = m.branchid group by b.idbranch order by b.idbranch desc;";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@startdate", param.startDate);
            dynamicParam.Add("@enddate", param.endDate);
            return _dbHelper.Query<int>(storeName, dynamicParam, System.Data.CommandType.Text);
        }

        /// <summary>
        /// báo cáo 1
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<SaleOrder> getReportRevenueByYear(ReportRevenueByYearParam param)
        {
            string storeName = "Proc_GetReportRevenueByYear";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("v_branchid", param.branchid);
            dynamicParam.Add("v_year", param.year);
            return _dbHelper.Query<SaleOrder>(storeName, dynamicParam, System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// thêm đơn hàng
        /// </summary>
        /// <param name="order"></param>
        /// <param name="listOrderDetail"></param>
        /// <returns></returns>
        public SaleOrder InsertOrderDetail(SaleOrder order, List<OrderDetail> listOrderDetail, List<ProductDetail> listProductDetail)
        {
            var result = Insert(order);
            if (result != null)
            {
                order = (SaleOrder)result;
                order = GetEntityByProperty(nameof(order.ordercode), order.ordercode);
                if(order != null)
                {
                    foreach (var item in listOrderDetail)
                    {
                        item.idsaleorder = order.idsaleorder;
                    }
                    bool isSuccess = _dbHelper.InsertBulk(listOrderDetail);

                }
                if(order.ordertypeid == (int)OrderType.Direct)
                {
                    _dbHelper.UpdateBulk(listProductDetail);
                }
                return order;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Sửa đơn hàng
        /// </summary>
        /// <param name="product"></param>
        /// <param name="listProductDetail"></param>
        /// <returns></returns>
        public SaleOrder UpdateOrderDetail(SaleOrder product, List<OrderDetail> listOrderDetail)
        {
            var result = Update(product, product.idsaleorder);
            bool isSuccess = _dbHelper.UpdateBulk(listOrderDetail);
            if (isSuccess)
            {
                return product;
            }
            return null;
        }

        /// <summary>
        /// báo cáo hàng hóa được mua nhiều nhất ( top 10)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<ReportProductBestSell> getReportProductBestSell(TimeParam param)
        {
            string storeName = "Proc_GetReportProductBestSell";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("v_startdate", param.startDate);
            dynamicParam.Add("v_enddate", param.endDate);
            return _dbHelper.Query<ReportProductBestSell>(storeName, dynamicParam, System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// laasy danh sach productdetail
        /// </summary>
        /// <param name="listProductID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ProductDetail> GetListProductDetailByListID(string listProductID)
        {
            string query = "SELECT * FROM productdetail p WHERE CONCAT(',',@listProductID,',') LIKE CONCAT('%,',p.idproductdetail,',%');";
            DynamicParameters dynamicParam = new DynamicParameters();
            dynamicParam.Add("@listProductID", listProductID);
            return _dbHelper.Query<ProductDetail>(query, dynamicParam, System.Data.CommandType.Text);
        }
    }
}
