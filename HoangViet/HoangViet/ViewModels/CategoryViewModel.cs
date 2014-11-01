using HoangViet.Models.Catalog;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoangViet.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
          
        }
        public string CategoryName { get; set; }

        public int ProductCount { get; set; }

        public virtual IPagedList<HoangViet.Models.Catalog.Product> Products { get; set; }
    }
}