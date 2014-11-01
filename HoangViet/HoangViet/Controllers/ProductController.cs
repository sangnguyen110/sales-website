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

namespace HoangViet.Controllers
{
    public class ProductController : BaseController
    {
        // GET: /Product/
        public async Task<ActionResult> Index()
        {
            var products = Db.Products.Include(p => p.Category);
            return View(await products.ToListAsync());
        }

        // GET: /Product/Details/5
        public async Task<ActionResult> Details(string category, int id, string slug)
        {           
            Product product = await Db.Products.Include(p=>p.Category).SingleOrDefaultAsync(p=> p.Id == id && p.Published) ;
            if (product == null)
            {
                return HttpNotFound();
            }
            if (product.CategoryId.HasValue)
            product.Category.Products = product.Category.Products.Where(p => p.Published && p.Id != product.Id)
                .OrderByDescending(p=>p.DisplayOrder)
                .ThenByDescending(p=>p.Id).Take(6)
                .ToList();
            ViewBag.Title = product.MetaTitle;
            ViewBag.Description = product.MetaDescription;
            ViewBag.Keywords = product.MetaKeywords;
            return View(product);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
