using HoangViet.Models.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoangViet.Areas.Orders.ViewModels
{
    public class OrderPaymentViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Ngày thanh toán")]
       
        /// <summary>
        /// Gets or sets the paid date and time
        /// </summary>
        public DateTime? PaidDateUtc { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        /// <summary>
        /// Gets or sets the payment status identifier
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        /// <summary>
        /// Gets or sets the payment method system name
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }
    }
}