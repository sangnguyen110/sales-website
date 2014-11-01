using HoangViet.Areas.Admin.Controllers;
using HoangViet.Models;
using HoangViet.Models.Orders;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using HoangViet.Models.Payments;
using PagedList;
using HoangViet.Models.Shipping;
using HoangViet.Areas.Shipping.ViewModels;
using System.Collections.Generic;
using System;
using HoangViet.Areas.Orders.ViewModels;

namespace HoangViet.Areas.Orders.Controllers
{
    public class OrderController : BaseAdminController
    {
        private HoangVietDbContext db;
        private void GetDbContext()
        {
            var owinContext = this.HttpContext.GetOwinContext();
            db = owinContext.Get<HoangVietDbContext>();
        }

        // GET: /Orders/Order/
        public ActionResult Index(int? page)
        {
            GetDbContext();
            var orders = db.Orders.Include(o => o.BillingAddress).Include(o => o.Customer).Include(o => o.ShippingAddress)
                .OrderByDescending(o => o.OrderDate);
            var pageNumber = page ?? 1;
            var onePageOfOrders = orders.ToPagedList(pageNumber,5);
            return View(onePageOfOrders);
        }

        // GET: /Orders/Order/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetDbContext();
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /Orders/Order/Create
        public ActionResult Create()
        {
            ViewBag.BillingAddressId = new SelectList(db.Addresses, "Id", "FullName");
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name");
            ViewBag.ShippingAddressId = new SelectList(db.Addresses, "Id", "FullName");
            return View();
        }

        // POST: /Orders/Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,CustomerId,OrderDate,BillingAddressId,ShippingAddressId,OrderStatusId,ShippingStatusId,PaymentStatusId,PaymentMethod,VatNumber,OrderSubtotalInclTax,OrderSubtotalExclTax,OrderShippingInclTax,OrderShippingExclTax,TaxRates,OrderTax,OrderTotal,PaidDateUtc,OrderStatus,PaymentStatus,ShippingStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BillingAddressId = new SelectList(db.Addresses, "Id", "FullName", order.BillingAddressId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", order.CustomerId);
            ViewBag.ShippingAddressId = new SelectList(db.Addresses, "Id", "FullName", order.ShippingAddressId);
            return View(order);
        }

        // GET: /Orders/Order/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetDbContext();
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.PaidDateUtc.HasValue)
                order.PaidDate = order.PaidDateUtc.Value.ToString("dd/MM/yyyy HH:mm");
            var shipmentViewModels = new List<ShipmentAdminViewModel>();
            foreach (var sm in order.Shipments)
            {
                var viewModel = new ShipmentAdminViewModel
                {
                    Id = sm.Id,
                    ShipperId = sm.ShipperId,
                    TotalWeight = sm.TotalWeight,
                    TrackingNumber = sm.TrackingNumber
                };
                if (sm.ShippedDateUtc.HasValue)
                    viewModel.ShippedDate = sm.ShippedDateUtc.Value.ToString("dd/MM/yyyy");
                if (sm.DeliveryDateUtc.HasValue)
                    viewModel.DeliveryDate = sm.DeliveryDateUtc.Value.ToString("dd/MM/yyyy");
                shipmentViewModels.Add(viewModel);
            }
            ViewBag.ShipmentViewModels = shipmentViewModels;
         
           // ViewBag.BillingAddress = await db.Addresses.FindAsync(order.Id);
            //ViewBag.BillingAddressId = new SelectList(db.Addresses, "Id", "FullName", order.BillingAddressId);
            //ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", order.CustomerId);
            //ViewBag.ShippingAddressId = new SelectList(db.Addresses, "Id", "FullName", order.ShippingAddressId);
            return View(order);
        }

        // POST: /Orders/Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,CustomerId,OrderDate,BillingAddressId,ShippingAddressId,OrderStatusId,ShippingStatusId,PaymentStatusId,PaymentMethod,VatNumber,OrderSubtotalInclTax,OrderSubtotalExclTax,OrderShippingInclTax,OrderShippingExclTax,TaxRates,OrderTax,OrderTotal,PaidDateUtc,OrderStatus,PaymentStatus,ShippingStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.BillingAddressId = new SelectList(db.Addresses, "Id", "FullName", order.BillingAddressId);
            //ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", order.CustomerId);
            //ViewBag.ShippingAddressId = new SelectList(db.Addresses, "Id", "FullName", order.ShippingAddressId);
            return View(order);
        }

        // GET: /Orders/Order/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Orders/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GetDbContext();
            Order order = await db.Orders.FindAsync(id);
            foreach (var sm in order.Shipments) {
                sm.OrderId = null;
            }
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> UpdatePaymentInfo([Bind(Include = "Id,PaidDate,PaymentStatus,PaymentMethod")] Order orderPaymentViewModel)
        {
            GetDbContext();
            var order = await db.Orders.FindAsync(orderPaymentViewModel.Id);
            if (order == null)
                return HttpNotFound();
            if (ModelState.IsValid)
            {
                order.PaymentStatus = orderPaymentViewModel.PaymentStatus;
                if (orderPaymentViewModel.PaidDate != null)
                    order.PaidDateUtc = DateTime.ParseExact(orderPaymentViewModel.PaidDate, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                else
                    order.PaidDateUtc = null;
                order.UpdateOrderStatus();
                await db.SaveChangesAsync();
            }
                return RedirectToAction("edit", new { id = order.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateOrderStatus(int? id, OrderStatus orderStatus)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetDbContext();
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            order.OrderStatus = orderStatus;
            await db.SaveChangesAsync();
            return RedirectToAction("edit", new { id = order.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateTaxRates(int? id, decimal taxRates)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetDbContext();
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.TaxRates = taxRates;
            order.CalOrderAmount();
            await db.SaveChangesAsync();
            return RedirectToAction("edit", new { id = order.Id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
