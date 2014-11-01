using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoangViet.Models.Shipping;
using HoangViet.Models;
using HoangViet.Areas.Admin.Controllers;

namespace HoangViet.Areas.Shipping.Controllers
{
    public class ShippingMethodController : BaseAdminController
    {
        private HoangVietDbContext db = new HoangVietDbContext();

        // GET: /Shipping/ShippingMethod/
        public async Task<ActionResult> Index()
        {
            return View(await db.ShippingMethods.ToListAsync());
        }

        // GET: /Shipping/ShippingMethod/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingMethod shippingmethod = await db.ShippingMethods.FindAsync(id);
            if (shippingmethod == null)
            {
                return HttpNotFound();
            }
            return View(shippingmethod);
        }

        // GET: /Shipping/ShippingMethod/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Shipping/ShippingMethod/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,ShippingMethodName,Description,ContactName,Sex,Phone")] ShippingMethod shippingmethod)
        {
            if (ModelState.IsValid)
            {
                db.ShippingMethods.Add(shippingmethod);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shippingmethod);
        }

        // GET: /Shipping/ShippingMethod/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingMethod shippingmethod = await db.ShippingMethods.FindAsync(id);
            if (shippingmethod == null)
            {
                return HttpNotFound();
            }
            return View(shippingmethod);
        }

        // POST: /Shipping/ShippingMethod/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,ShippingMethodName,Description,ContactName,Sex,Phone")] ShippingMethod shippingmethod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shippingmethod).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shippingmethod);
        }

        // GET: /Shipping/ShippingMethod/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingMethod shippingmethod = await db.ShippingMethods.FindAsync(id);
            if (shippingmethod == null)
            {
                return HttpNotFound();
            }
            return View(shippingmethod);
        }

        // POST: /Shipping/ShippingMethod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShippingMethod shippingmethod = await db.ShippingMethods.FindAsync(id);
            foreach (var shipment in shippingmethod.Shipments)
            {
                shipment.ShipperId = null;
            }
            db.ShippingMethods.Remove(shippingmethod);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
