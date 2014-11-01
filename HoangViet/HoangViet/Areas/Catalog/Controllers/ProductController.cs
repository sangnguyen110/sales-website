using HoangViet.Areas.Admin.Controllers;
using HoangViet.Areas.Admin.ViewModels;
using HoangViet.Areas.Catalog.ViewModels;
using HoangViet.Models;
using HoangViet.Models.Catalog;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace HoangViet.Areas.Catalog.Controllers
{
    public class ProductController : BaseAdminController
    {
        private HoangVietDbContext db = new HoangVietDbContext();

        // GET: /Catalog/Product/
        public async Task<ActionResult> Index(int page = 1)
        {
            IList<Product> products = await db.Products.Include(p => p.Category).ToListAsync();
            var onePageProducts = products.ToPagedList(page, 10);
            return View(onePageProducts);
        }

        public async Task<ActionResult> IndexFor(int entityId, FromPageEnum frompage, int page = 1)
        {
            var alreadyInProductIds = new List<int>();
            if (frompage == FromPageEnum.OrderEdit)
            {
                var order = await db.Orders.FindAsync(entityId);
                if (order == null)
                    return HttpNotFound();
                foreach (var orderDetail in order.OrderDetails)
                {
                    alreadyInProductIds.Add(orderDetail.Product.Id);
                };
            }
            ViewBag.FromPageTo = new FromPageTo
            {
                EntityId = entityId,
                FromPage = frompage
            };
            var products = db.Products.Where(p => !alreadyInProductIds.Contains(p.Id)).Include(p => p.Category).ToList();

            var onePageProducts = products.ToPagedList(page, 10);
            return View(onePageProducts);
        }

        // GET: /Catalog/Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Catalog/Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: /Catalog/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductName,ShortDescription,FullDescription,CategoryId,ShowOnHomePage,MetaKeywords,MetaDescription,MetaTitle,IsShipEnabled,IsFreeShipping,IsFeaturedProduct,AdditionalShippingCharge,StockQuantity,Price,DisplayOrder,Published,ImageLink")] Product product)
        {
            product.Slug = IRONBEE.Tools.MakeSlug(product.ProductName);
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: /Catalog/Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: /Catalog/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProductName,ShortDescription,FullDescription,CategoryId,ShowOnHomePage,MetaKeywords,MetaDescription,MetaTitle,IsShipEnabled,IsFreeShipping,AdditionalShippingCharge,StockQuantity,Price,DisplayOrder,Published,ImageLink,IsFeaturedProduct")] Product product)
        {
            product.Slug = IRONBEE.Tools.MakeSlug(product.ProductName);
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: /Catalog/Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Catalog/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
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
