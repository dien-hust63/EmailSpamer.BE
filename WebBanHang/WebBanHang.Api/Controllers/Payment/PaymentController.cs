using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.BL;
using PaymentGateway.Interface;
using PaymentGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Api.Controllers
{
    public class PaymentController : BaseEntityController<Payment>
    {
        IBaseBL<Payment> _baseBL;
        IPaymentBL _paymentBL;
        public PaymentController(IBaseBL<Payment> baseBL, IPaymentBL paymentBL) : base(baseBL)
        {
            _baseBL = baseBL;
            _paymentBL = paymentBL;
        }

        [HttpPost("redirect-vnpay")]
        public ServiceResult redirectVNPay(Payment payment)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _paymentBL.paymentVNPay(payment);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.Message);
            }
            return serviceResult;
        }


    }
}
