using Gather.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using PaymentGateway.Interface;
using PaymentGateway.Library;
using PaymentGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.ServiceCollection;

namespace PaymentGateway.BL
{
    public class PaymentBL : IPaymentBL
    {
        protected IConfiguration _configuration;
        protected IDBHelper _dbHelper;
        public PaymentBL(IConfiguration configuration, IDBHelper dbHelper)
        {
            _configuration = configuration;
            _dbHelper = dbHelper;

        }
        public ServiceResult paymentVNPay(Payment payment)
        {
            ServiceResult serviceResult = new ServiceResult();
            var vnPayInfo = _configuration.GetSection("VNPay");  
            //Get Config Info
            string vnp_Returnurl = vnPayInfo["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = vnPayInfo["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = vnPayInfo["vnp_TmnCode"]; //Ma website
            string vnp_HashSecret = vnPayInfo["vnp_HashSecret"]; //Chuoi bi mat
            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                serviceResult.setError("Chưa cấu hình vnp_TmnCode hoặc vnp_HashSecret");
                return serviceResult;
            }
            //Get payment input
            OrderInfo order = new OrderInfo();
            //Save order to db
            order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = payment.Amount; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending"
            order.OrderDesc = "test";
            order.CreatedDate = DateTime.Now;
            string locale = "vn";
            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            //vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            if (!string.IsNullOrEmpty(locale))
            {
                vnpay.AddRequestData("vnp_Locale", locale);
            }
            else
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            //Billing
            vnpay.AddRequestData("vnp_Bill_Mobile", "0123456789");
            vnpay.AddRequestData("vnp_Bill_Email", "vnpaytest@vnpay.vn");
            var fullName = "Nguyen Van A";
            if (!String.IsNullOrEmpty(fullName))
            {
                var indexof = fullName.IndexOf(' ');
                vnpay.AddRequestData("vnp_Bill_FirstName", fullName.Substring(0, indexof));
                vnpay.AddRequestData("vnp_Bill_LastName", fullName.Substring(indexof + 1, fullName.Length - indexof - 1));
            }
            vnpay.AddRequestData("vnp_Bill_Address", "22 Lang Ha");
            vnpay.AddRequestData("vnp_Bill_City", "Hà Nội");
            vnpay.AddRequestData("vnp_Bill_Country", "VN");
            vnpay.AddRequestData("vnp_Bill_State", "");

            // Invoice

            vnpay.AddRequestData("vnp_Inv_Phone", "02437764668");
            vnpay.AddRequestData("vnp_Inv_Email", "vnpaytest@vnpay.vn");
            vnpay.AddRequestData("vnp_Inv_Customer", "Nguyen Van A");
            vnpay.AddRequestData("vnp_Inv_Address", "22 Láng Hạ, Phường Láng Hạ, Quận Đống Đa, TP Hà Nội");
            vnpay.AddRequestData("vnp_Inv_Company", "Công ty Cổ phần giải pháp Thanh toán Việt Nam");
            vnpay.AddRequestData("vnp_Inv_Taxcode", "0102182292");
            vnpay.AddRequestData("vnp_Inv_Type", "I");

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            serviceResult.Data = paymentUrl;
            return serviceResult;
        }
    }
}
