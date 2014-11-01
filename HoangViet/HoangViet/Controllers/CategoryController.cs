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
using HoangViet.ViewModels;
using PagedList;

namespace HoangViet.Controllers
{
    public class CategoryController : BaseController
    {
      

        // GET: /Categroy/
        public async Task<ActionResult> Search(string searchTerm, int? searchCaterory, int page = 1)
        {
            var categoryViewModel = new CategoryViewModel(){
            CategoryName = "Tìm kiếm",            
            };
            var encodeSearchTerm = IRONBEE.Tools.ToAlias(searchTerm);
            if (searchCaterory.HasValue)
            {
                var category = await Db.Categories.Include(c => c.Products).SingleOrDefaultAsync(c => c.Published && c.Id == searchCaterory);
              categoryViewModel.Products = category.Products.Where(p=>p.Published && IRONBEE.Tools.ToAlias(p.ProductName).Contains(searchTerm))
                   .OrderByDescending(p => p.DisplayOrder).ThenByDescending(p => p.Id)
                    .ToPagedList(page, 2);
            }
            else {
                var allProducts = await Db.Products.Where(p => p.Published).ToListAsync();
                categoryViewModel.Products = allProducts.Where(p => IRONBEE.Tools.ToAlias(p.ProductName).Contains(encodeSearchTerm))
                    .OrderByDescending(p => p.DisplayOrder).ThenByDescending(p => p.Id)
                    .ToPagedList(page, 2);
            }
            categoryViewModel.ProductCount = categoryViewModel.Products.Count;
            return View("details",categoryViewModel);
        }

        // GET: /Categroy/Details/5
        public async Task<ActionResult> Details(string category, int page = 1)
        {
            Category getCategory = await Db.Categories.Include(c => c.Products).SingleOrDefaultAsync(c => c.Slug == category && c.Published);
            if (getCategory == null)
            {
                return HttpNotFound();
            }
            var categoryViewModel = new CategoryViewModel { 
            CategoryName = getCategory.CategoryName,
            ProductCount = getCategory.Products.Count,
            Products = getCategory.Products.Where(p => p.Published)
            .OrderByDescending(p => p.DisplayOrder).ThenByDescending(p => p.Id)
            .ToPagedList(page, 6)
            };           
            ViewBag.Title = getCategory.MetaTitle;
            ViewBag.Description = getCategory.MetaDescription;
            ViewBag.Keywords = getCategory.MetaKeywords;
            return View(categoryViewModel);
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
