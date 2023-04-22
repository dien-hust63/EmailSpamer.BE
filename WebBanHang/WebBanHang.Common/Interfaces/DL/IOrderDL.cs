using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.DL
{
    public interface IOrderDL : IBaseDL<SaleOrder>
    {
        /// <summary>
        /// Thêm mới product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="listProductDetail"></param>
        /// <returns></returns>
        public SaleOrder InsertOrderDetail(SaleOrder order, List<OrderDetail> listOrderDetail, List<ProductDetail> listProductDetail);

        /// <summary>
        /// Lấy chi tiết đơn hàng
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        object getOrderDetail(int entityId);

        /// <summary>
        /// Sửa đơn hàng
        /// </summary>
        /// <param name="order"></param>
        /// <param name="listOrderDetail"></param>
        /// <returns></returns>
        public SaleOrder UpdateOrderDetail(SaleOrder order, List<OrderDetail> listOrderDetail);

        /// <summary>
        /// lấy thông order code cuối ucngf
        /// </summary>
        /// <returns></returns>
        public SaleOrder GetLastestOrder();

        /// <summary>
        /// trả về báo cáo doanh thu 1
        /// </summary>
        /// <param name="param"></param>
        public List<SaleOrder> getReportRevenueByYear(ReportRevenueByYearParam param);

        /// <summary>
        /// lấy doanh số theo chi nhánh 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<int> getReportRevenueByBranch(TimeParam param);

        /// <summary>
        /// báo cáo hàng hóa được mua nhiều nhất ( top 10)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<ReportProductBestSell> getReportProductBestSell(TimeParam param);

        public List<ProductDetail> GetListProductDetailByListID(string listProductID);

    }
}
