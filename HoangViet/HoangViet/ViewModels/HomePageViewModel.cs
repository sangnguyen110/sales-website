using HoangViet.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoangViet.ViewModels
{
    public class HomepageViewModel
    {
        public HomepageViewModel()
        {
            this.FeatureProducts = new List<Product>();
            this.LatestProducts = new List<Product>();
        }
        public virtual ICollection<Product> FeatureProducts { get; set; }
        public int CountFeatureProducts { get; set; }
        public virtual ICollection<Product> LatestProducts { get; set; }
    }
}