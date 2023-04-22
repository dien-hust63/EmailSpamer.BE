using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebBanHang.Common.Enumeration
{
    public class Enumeration
    {
        public enum EntityState
        {
            Add = 1,
            Edit = 2,
            Delete = 3
        }

        public enum Operator
        {
            [Display(Name = "EQUAL")]
            EQUAL = 1,

            [Display(Name = "LIKE")]
            LIKE = 2
        }

        public enum OrderByType 
        { 
            ASC = 1,
            DESC = 2
        }

        public enum AccountStatus
        {
            [Display(Name = "Chưa kích hoạt")]
            NotActive = 1,

            [Display(Name = "Đã kích hoạt")]
            Active = 2,
        }

        public enum ProductSortOrder
        {
            [Display(Name = "Mới nhất")]
            Newest = 1,

            [Display(Name = "Giá thấp đến cao")]
            PriceAsc = 2,

            [Display(Name = "Giá cao xuống thấp")]
            PriceDes = 3
        }


        public enum OrderStatus
        {
            [Display(Name = "Thành công")]
            Success = 1,

            [Display(Name = "Chờ tiếp nhận")]
            WaitingReceive = 2,

            [Display(Name = "Đã tiếp nhận")]
            Received = 3,

            [Display(Name = "Chờ giao hàng")]
            WaitingDeliver = 4,

            [Display(Name = "Đang giao hàng")]
            DoingDeliver = 5,

            [Display(Name = "Giao hàng thất bại")]
            DeliverFail = 6,

            [Display(Name = "Đổi hàng")]
            Exchange = 7,

            [Display(Name = "Trả hàng")]
            Return = 8,

            [Display(Name = "Hủy")]
            Destroy = 9,

        }

        public enum CheckoutType
        {
            [Display(Name = "Thanh toán qua VNPAY")]
            VNPAY = 1,

            [Display(Name = "Thanh toán khi nhận hàng")]
            COD = 2,
        }

        public enum CheckoutStatus
        {
            [Display(Name = "Đã thanh toán")]
            Done = 1,

            [Display(Name = "Chưa thanh toán")]
            NotPay = 2,
        }

        public enum OrderType
        {
            [Display(Name = "Mua trực tiếp tại chi nhánh")]
            Direct = 1,

            [Display(Name = "Mua online")]
            Online = 2,
        }


        public enum EmailCampaignStatus
        {

            [Display(Name = "Chưa gửi")]
            Wait = 1,

            [Display(Name = "Đã gửi")]
            Sent = 2,

            [Display(Name = "Từ chối")]
            Unsubcribe = 3,
        }
    }
}
