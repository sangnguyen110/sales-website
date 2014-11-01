using HoangViet.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoangViet.ViewModels
{
    public class CheckoutViewModel
    {
        public virtual Address BillingAddress { get; set; }
        public virtual Address ShippingAddress { get; set; }

    }
}