using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoangViet.Models.Payments
{
    public enum PaymentMethod:int
    {
        [Display(Name = "Thanh toán tại cửa hàng")]
        /// <summary>
        /// Cash at store
        /// </summary>
        CashAtStore = 10,
        [Display(Name = "Cổng thanh toán ngân lượng")]
        /// <summary>
        /// Authorized
        /// </summary>
        Paypal = 20,
    }
}