using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoangViet.Models.Catalog;
using HoangViet.Models;
using HoangViet.Areas.Admin.Controllers;
using HoangViet.Areas.Catalog.ViewModels;
//using Microsoft.AspNet.Identity.Owin;

namespace HoangViet.Areas.Catalog.Controllers
{
    public class CategoryController : BaseAdminController
    {
       private HoangVietDbContext db = new HoangVietDbContext();
      

     
       
        // GET: /Catalog/Category/
        public async Task<ActionResult> Index()
        {
            //var owin = this.HttpContext.GetOwinContext();

            //db = owin.Get<HoangVietDbContext>();
            //var userManager = owin.GetUserManager<HoangViet.Models.Accounts.HoangVietUserManager>();
            //var user = await userManager.FindByNameAsync(User.Identity.Name);
            //var userId = user.Id;
            var categories = db.Categories.Include(c => c.ParentCategory);
            return View(await categories.ToListAsync());
        }

        // GET: /Catalog/Category/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Catalog/Category/Create
        public ActionResult Create()
        {
            ViewBag.ParentCategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: /Catalog/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CategoryName,MetaKeywords,MetaDescription,MetaTitle,ParentCategoryId,Published,DisplayOrder,ShowOnHomePage")] Category category)
        {
            category.Slug = IRONBEE.Tools.MakeSlug(category.CategoryName);
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ParentCategoryId = new SelectList(db.Categories, "Id", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: /Catalog/Category/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentCategoryId = new SelectList(db.Categories.Where(c => c.Id != id) , "Id", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // POST: /Catalog/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CategoryName,MetaKeywords,MetaDescription,MetaTitle,ParentCategoryId,Published,DisplayOrder,ShowOnHomePage")] Category category)
        {

            category.Slug = IRONBEE.Tools.MakeSlug(category.CategoryName);
            if (category.Id != category.ParentCategoryId && ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentCategoryId = new SelectList(db.Categories.Where(c => c.Id != category.Id), "Id", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: /Catalog/Category/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Catalog/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            foreach (var childCategory in category.ChildCategories)
            {
                childCategory.ParentCategory = null;
            }
            db.Categories.Remove(category);
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
