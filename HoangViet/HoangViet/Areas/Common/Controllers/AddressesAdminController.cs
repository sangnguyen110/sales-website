using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoangViet.Models.Common;
using HoangViet.Models;
using HoangViet.Areas.Admin.Controllers;

namespace HoangViet.Areas.Common.Controllers
{
    public class AddressesAdminController : BaseAdminController
    {
        private HoangVietDbContext db = new HoangVietDbContext();

        // GET: /Common/AddressesAdmin/
        public async Task<ActionResult> Index()
        {
            var addresses = db.Addresses.Include(a => a.City).Include(a => a.Customer).Include(a => a.District);
            return View(await addresses.ToListAsync());
        }

        // GET: /Common/AddressesAdmin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: /Common/AddressesAdmin/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Citys, "Id", "CityName");
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name");
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName");
            return View();
        }

        // POST: /Common/AddressesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FullName,Email,Company,CityId,DistrictId,Address1,PhoneNumber,FaxNumber,DefaultBillingAddress,DefaultShippingAddress,CustomerId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Citys, "Id", "CityName", address.CityId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", address.CustomerId);
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName", address.DistrictId);
            return View(address);
        }

        // GET: /Common/AddressesAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id, int? orderId)
        {
            ViewBag.OrderId = orderId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Citys, "Id", "CityName", address.CityId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", address.CustomerId);
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName", address.DistrictId);
            ViewBag.Districts = db.Districts.Select(d => new
            {
                DistrictId = d.Id,
                DistrictName = d.DistrictName,
                CityId = d.CityId
            });
            return View(address);
        }

        // POST: /Common/AddressesAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FullName,Email,Company,CityId,DistrictId,Address1,PhoneNumber,FaxNumber,DefaultBillingAddress,DefaultShippingAddress,CustomerId")] Address address, int? orderId)
        {
            ViewBag.OrderId = orderId;
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (orderId != null)
                    return RedirectToAction("edit", new { area = "orders", controller = "order", id = orderId });
                else
                    return RedirectToAction("index");
            }
            ViewBag.CityId = new SelectList(db.Citys, "Id", "CityName", address.CityId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", address.CustomerId);
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName", address.DistrictId);
            ViewBag.Districts = db.Districts.Select(d => new { 
            DistrictId = d.Id,
            DistrictName = d.DistrictName,
            CityId = d.CityId
            });
            return View(address);
        }

        // GET: /Common/AddressesAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Common/AddressesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Address address = await db.Addresses.FindAsync(id);
            db.Addresses.Remove(address);
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
