using Gather.ApplicationCore.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Entities.Param;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Services;
using WebBanHang.DL.DL;

namespace WebBanHang.BL.BL
{
    public class OrderBL : BaseBL<SaleOrder>, IOrderBL
    {
        IOrderDL _orderDL;

        public OrderBL(IBaseDL<SaleOrder> baseDL, IOrderDL orderDL) : base(baseDL)
        {
            _orderDL = orderDL;
        }

        /// <summary>
        /// thêm đơn hàng
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult InsertOrderDetail(OrderDetailParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            SaleOrder order = new SaleOrder();
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            order = JsonSerializer.Deserialize<SaleOrder>(param.order);
            listOrderDetail = JsonSerializer.Deserialize<List<OrderDetail>>(param.orderdetail);
            List<ProductDetail> listProductDetail = _orderDL.GetListProductDetailByListID(string.Join(",", listOrderDetail.Select(x => x.idproductdetail).ToArray()));
            serviceResult = this.validateOrder(order, listOrderDetail, listProductDetail);
            if (!serviceResult.Success)
            {
                return serviceResult;
            }
            // Tạo mã đơn hàng tự động
            order.ordercode = CreateAutoOrderCode();

            
            SaleOrder? result = _orderDL.InsertOrderDetail(order, listOrderDetail, listProductDetail);
            if(order != null)
            {
                // gửi mail
            }


            serviceResult.Data = result;
            return serviceResult;
        }
        public ServiceResult validateOrder(SaleOrder order, List<OrderDetail> orderDetail, List<ProductDetail> listProductDetail)
        {
            ServiceResult serviceResult = new ServiceResult();
            
            List<string> productcode = new List<string>();
            foreach (var item in orderDetail)
            {
                ProductDetail currentProduct =  listProductDetail.Find(x => x.idproductdetail == item.idproductdetail);
                int inventory = currentProduct != null ? currentProduct.inventory.Value : 0;
                if(item.quantity > inventory)
                {
                    productcode.Add(item.productcode);
                }
            }
            if(productcode.Count > 0)
            {
                string errorProduct = string.Join(",", productcode.ToArray());
                serviceResult.Success = false;
                serviceResult.ErrorMessage = $"{errorProduct} vượt quá số lượng tồn kho";
                return serviceResult;
            }
            foreach (var item in listProductDetail)
            {
                OrderDetail currentProduct = orderDetail.Find(x => x.idproductdetail == item.idproductdetail);
                int quantity = currentProduct != null ? currentProduct.quantity : 0;
                item.inventory -= quantity;
            }
            return serviceResult;
        }
        public string CreateAutoOrderCode()
        {
            string newCode = "";
            SaleOrder lastOrder = _orderDL.GetLastestOrder(); 
            if(lastOrder != null)
            {
                string lastCode = lastOrder.ordercode;
                int codenumber = int.Parse(lastCode.Substring(2)) + 1;
                newCode = "DH" + codenumber.ToString();
            }
            else
            {
                newCode = "DH100001";
            }
            return newCode;
        }



        /// <summary>
        /// Lấy chi tiết đơn hàng
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult getOrderDetail(int entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _orderDL.getOrderDetail(entityId);
            return serviceResult;
        }


        /// <summary>
        /// Cập nhật đơn hàng
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ServiceResult UpdateOrderDetail(OrderDetailParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            SaleOrder order = new SaleOrder();
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            order = JsonSerializer.Deserialize<SaleOrder>(param.order);
            listOrderDetail = JsonSerializer.Deserialize<List<OrderDetail>>(param.orderdetail);
            SaleOrder? result = _orderDL.UpdateOrderDetail(order, listOrderDetail);



            serviceResult.Data = result;
            return serviceResult;
        }


        /// <summary>
        /// lấy báo cáo doanh thu theo chi nhánh từng năm
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<double> getReportRevenueByYear(ReportRevenueByYearParam param)
        {
            List<double> result = new List<double>();
            // trả về list order theo chi nhánh trong năm của param
            List<SaleOrder> listSaleOrder = _orderDL.getReportRevenueByYear(param);
            for (int i = 1; i <= 12; i++)
            {
                int revenue = listSaleOrder.Where(x => x.orderdate.Value.Month == i).Sum(x => x.totalprice);
                double output = (double)revenue / 1000000;
                output = Math.Round(output, 1);
                result.Add(output);
            }
            return result;
        }

        /// <summary>
        /// lấy báo cáo doanh thu các chi nhánh biểu đồ tròn
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<double> getReportRevenueByBranch(TimeParam param)
        {
            List<double> result = new List<double>();
            List<int> listRevenue = _orderDL.getReportRevenueByBranch(param);
            foreach (var item in listRevenue)
            {
                double output = (double)item / 1000000;
                output = Math.Round(output, 1);
                result.Add(output);

            }
            return result;
        }

        public List<ReportProductBestSell> getReportProductBestSell(TimeParam param)
        {
            List<ReportProductBestSell> listProduct = _orderDL.getReportProductBestSell(param);
            listProduct = listProduct.OrderBy(x => x.quantity).ToList();
            return listProduct;
        }
    }
}
