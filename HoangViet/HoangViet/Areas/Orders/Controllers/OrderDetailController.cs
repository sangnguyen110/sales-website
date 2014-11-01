using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoangViet.Models.Orders;
using HoangViet.Models;
using HoangViet.Areas.Admin.Controllers;

namespace HoangViet.Areas.Orders.Controllers
{
    public class OrderDetailController : BaseAdminController
    {
        private HoangVietDbContext db = new HoangVietDbContext();

        // GET: /Orders/OrderDetail/
        public async Task<ActionResult> Index()
        {
            var orderdetails = db.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await orderdetails.ToListAsync());
        }

        // GET: /Orders/OrderDetail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = await db.OrderDetails.FindAsync(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // GET: /Orders/OrderDetail/Create
        public  async Task<ActionResult> Create(int orderId, int productid)
        {
            var order = await db.Orders.FindAsync(orderId);
             var product = await db.Products.FindAsync(productid);
            if (order == null || product == null)
                return HttpNotFound();
            var orderDetail = new OrderDetail
            {
                OrderId = order.Id,
                ProductId = product.Id,
                UnitPriceExclTax = product.Price
            };
            return View(orderDetail);
        }

        // POST: /Orders/OrderDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,OrderId,ProductId,Quantity,UnitPriceInclTax,UnitPriceExclTax,PriceInclTax,PriceExclTax")] OrderDetail orderdetail)
        {
            var order = await db.Orders.FindAsync(orderdetail.OrderId);
            var product = await db.Products.FindAsync(orderdetail.ProductId);
            if (order == null || product == null)
                return HttpNotFound();
            if (ModelState.IsValid)
            {
                var newOrderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = orderdetail.Quantity,
                    UnitPriceExclTax = orderdetail.UnitPriceExclTax
                };
                newOrderDetail.CalOrderDetailAmounts();
                order.OrderDetails.Add(newOrderDetail);
                order.UpdateShippingStatus();
                order.UpdateOrderStatus();
                order.CalOrderAmount();
                await db.SaveChangesAsync();
                return RedirectToAction("edit", new { area = "orders", controller = "order", id = newOrderDetail.OrderId }); ;
            }

            return View(orderdetail);
        }

        // GET: /Orders/OrderDetail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = await db.OrderDetails.FindAsync(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            //ViewBag.OrderId = new SelectList(db.Orders, "Id", "CustomerId", orderdetail.OrderId);
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", orderdetail.ProductId);
            return View(orderdetail);
        }

        // POST: /Orders/OrderDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,OrderId,ProductId,Quantity,UnitPriceInclTax,UnitPriceExclTax,PriceInclTax,PriceExclTax")] OrderDetail orderDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderDetail = await db.OrderDetails.FindAsync(orderDetailViewModel.Id);
                orderDetail.UnitPriceExclTax = orderDetailViewModel.UnitPriceExclTax;
                orderDetail.Quantity = orderDetailViewModel.Quantity;
                orderDetail.CalOrderDetailAmounts();
                orderDetail.Order.UpdateShippingStatus();
                orderDetail.Order.UpdateOrderStatus();
                orderDetail.Order.CalOrderAmount();
                await db.SaveChangesAsync();
                return RedirectToAction("edit", new { area = "orders", controller = "order", id = orderDetail.OrderId });
            }
            //ViewBag.OrderId = new SelectList(db.Orders, "Id", "CustomerId", orderdetail.OrderId);
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", orderdetail.ProductId);
            return View(orderDetailViewModel);
        }

        // GET: /Orders/OrderDetail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = await db.OrderDetails.FindAsync(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // POST: /Orders/OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await db.OrderDetails.FindAsync(id);
            var order = orderDetail.Order;
            db.OrderDetails.Remove(orderDetail);
            order.UpdateShippingStatus();
            order.UpdateOrderStatus();
            order.CalOrderAmount();
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("edit", new { area="orders", controller ="order", id = order.Id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
