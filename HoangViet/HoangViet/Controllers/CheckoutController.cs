using HoangViet.Models.Orders;
using HoangViet.Models.Shipping;
using HoangViet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HoangViet.Controllers
{
    public class CheckoutController : BaseController
    {
        //
        // GET: /Checkout/
        [Authorize]
        public async Task<ActionResult> Index()
        {
             var customer = await UserManager.FindByNameAsync(this.User.Identity.Name);
            
            var model = new CheckoutViewModel();
            model.BillingAddress =  Db.Addresses.FirstOrDefault(a => a.CustomerId == customer.Id && a.DefaultBillingAddress);
            model.ShippingAddress = Db.Addresses.FirstOrDefault(a => a.CustomerId == customer.Id && a.DefaultShippingAddress);
            ViewData["BillingAddress.CityId"] = new SelectList(Db.Citys, "Id", "CityName",model.BillingAddress.CityId);
            ViewData["BillingAddress.DistrictId"] = new SelectList(Db.Districts, "Id", "DistrictName", model.BillingAddress.DistrictId);
            ViewData["ShippingAddress.CityId"] = new SelectList(Db.Citys, "Id", "CityName", model.BillingAddress.CityId);
            ViewData["ShippingAddress.DistrictId"] = new SelectList(Db.Districts, "Id", "DistrictName", model.BillingAddress.DistrictId);
            ViewBag.Districts = Db.Districts.Select(d => new
            {
                DistrictId = d.Id,
                DistrictName = d.DistrictName,
                CityId = d.CityId
            });
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(CheckoutViewModel model)
        {
            var customer = await UserManager.FindByNameAsync(this.User.Identity.Name);
            var billingAddress = model.BillingAddress;
            var shippingAddress = model.ShippingAddress;
            billingAddress.CustomerId = customer.Id;
            shippingAddress.CustomerId = customer.Id;
            Db.Addresses.Add(billingAddress);
            Db.Addresses.Add(shippingAddress);
            var order = new Order
            {
                BillingAddress = billingAddress,
                Customer = customer,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Pending,
                TaxRates = 10,
                ShippingStatus = ShippingStatus.NotYetShipped,
                PaymentStatus = Models.Payments.PaymentStatus.Pending,
                PaymentMethod = Models.Payments.PaymentMethod.Paypal,
                ShippingAddress = shippingAddress
            };
            var cart = new ShoppingCart(HttpContext, Db);
            order = await cart.CreateOrderAsync(order);
            Db.Orders.Add(order);
            await Db.SaveChangesAsync();
            return RedirectToAction("ValidateCommand","PayPal",new { orderId= order.Id, totalPrice = order.OrderTotal});
        }
    }
}