using HoangViet.Models.Payments;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoangViet.Controllers
{
    public class PayPalController : BaseController
    {
        [Authorize]
        public ActionResult ValidateCommand(string orderId, string totalPrice)
        {
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
            var paypal = new PayPalModel(useSandbox);

            paypal.invoice = orderId;
            paypal.amount = totalPrice;
            paypal.item_name = "Đơn hàng số " + orderId;
            return View(paypal);
        }

        public ActionResult RedirectFromPaypal()
        {
            return View();
        }

        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        public ActionResult NotifyFromPaypal()
        {
            return View();
        }

         //<add key="business" value="asrce2_1311074442_biz@gmail.com" />
    }
}
