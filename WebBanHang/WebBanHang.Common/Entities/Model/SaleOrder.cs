using Gather.ApplicationCore.Constant;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Common.Entities.Model
{
    public class SaleOrder
    {
        [AttributeCustomId]
        public int idsaleorder { get; set; }

        /// <summary>
        /// mã đơn hàng
        /// </summary>
        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Mã đơn hàng")]
        public string ordercode { get; set; }

        /// <summary>
        /// tên khách hàng
        /// </summary>
        public string customername { get; set; }

        /// <summary>
        /// sđt khách hàng
        /// </summary>
        public string customerphone { get; set; }

        /// <summary>
        /// email khách hàng
        /// </summary>
        public string customeremail { get; set; }

        /// <summary>
        /// ghi chú của khách
        /// </summary>
        public string customerdescription { get; set; }

        public int provinceid { get; set; }

        public string provincename { get; set; }

        public int districtid { get; set; }

        public string districtname { get; set; }

        public string wardid { get; set; }

        public string wardname { get; set; }

        /// <summary>
        /// Địa chỉ giao hàng
        /// </summary>
        public string customeraddress { get; set; }

        public int deliverprice { get; set; }

        public int totalprice { get; set; }

        /// <summary>
        /// ngày đặt hàng
        /// </summary>
        public DateTime? orderdate { get; set; }

        /// <summary>
        /// nhân viên tiếp nhận
        /// </summary>
        public int receiveemployeeid { get; set; }

        /// <summary>
        /// tên nhân viên tiếp nhận
        /// </summary>
        public string receiveemployeename { get; set; }

        /// <summary>
        /// chi nhánh
        /// </summary>
        public int branchid { get; set; }

        /// <summary>
        /// chi nhánh bán
        /// </summary>
        public string branchname { get; set; }

        /// <summary>
        /// Hình thức mua hàng
        /// </summary>
        public int ordertypeid { get; set; }

        /// <summary>
        /// Hình thức mua hàng
        /// </summary>
        public string ordertypename {
            get
            {
                foreach (OrderType foo in Enum.GetValues(typeof(OrderType)))
                {
                    if (ordertypeid == (int)foo)
                    {
                        return foo.GetDisplayName();
                    }
                }
                return "";
            }
        }

        /// <summary>
        /// trạng thái thanh toán
        /// </summary>
        public int checkoutstatusid { get; set; }

        /// <summary>
        /// Trạng thái thanh toán
        /// </summary>
        public string checkoutstatusname
        {
            get
            {
                foreach (CheckoutStatus foo in Enum.GetValues(typeof(CheckoutStatus)))
                {
                    if (checkoutstatusid == (int)foo)
                    {
                        return foo.GetDisplayName();
                    }
                }
                return "";
            }
        }

        /// <summary>
        /// Hình thức thanh toán
        /// </summary>
        public int checkouttypeid { get; set; }

        /// <summary>
        /// Hình thức thanh toán
        /// </summary>
        public string checkouttypename {
            get
            {
                foreach (CheckoutType foo in Enum.GetValues(typeof(CheckoutType)))
                {
                    if (checkouttypeid == (int)foo)
                    {
                        return foo.GetDisplayName();
                    }
                }
                return "";
            }
        }

        /// <summary>
        /// trạng thái đơn hàng
        /// </summary>
        public int statusid { get; set; }

        /// <summary>
        /// text trạng thái đơn hàng
        /// </summary>
        public string statusname
        {
            get
            {
                foreach (OrderStatus foo in Enum.GetValues(typeof(OrderStatus)))
                {
                    if(statusid == (int)foo)
                    {
                        return foo.GetDisplayName();
                    }
                }
                return "";
            }
        }


        [AttributeCustomNotMap]
        public string orderdatetext
        {
            get
            {
                return orderdate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
    }
}


