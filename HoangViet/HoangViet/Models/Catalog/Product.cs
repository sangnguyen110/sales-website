using HoangViet.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Mvc;

namespace HoangViet.Models.Catalog
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public partial class Product : BaseEntity
    {
        public Product()
        {
            //  this.OrderDetails = new List<OrderDetail>();
            // this.Pictures = new List<Picture>();
            this.Published = false;
            this.ShowOnHomePage = false;
            this.IsFreeShipping = false;
            this.IsShipEnabled = false;
        }
        [Display(Name = "Tên hàng hóa")]
        //  [StringLength(50)]
        [Required]
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string ProductName { get; set; }
        public string Slug { get; set; }
        [Display(Name = "Mô tả ngắn")]
        [StringLength(197)]
        [Required]
        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public string ShortDescription { get; set; }
        [Display(Name = "Mô tả đầy đủ")]
        [AllowHtml]
        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public string FullDescription { get; set; }

        ///// <summary>
        ///// Gets or sets the admin comment
        ///// </summary>
        //public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a vendor identifier
        /// </summary>
        //    public Nullable<int> ManufacturerId { get; set; }
        /// <summary>
        /// Gets or sets a vendor identifier
        /// </summary>
        public Nullable<int> CategoryId { get; set; }
        [Display(Name = "Hiển thị trên trang chủ")]
        /// <summary>
        /// Gets or sets a value indicating whether to show the product on home page
        /// </summary>
        public bool ShowOnHomePage { get; set; }
        [Display(Name = "SEO - từ khóa")]
        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }
        [Display(Name = "SEO - mô tả")]
        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }
        [Display(Name = "SEO - tiêu đề")]
        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        ///// <summary>
        ///// Gets or sets the manufacturer part number
        ///// </summary>
        //public string ManufacturerPartNumber { get; set; }
        [Display(Name = "Có giao hàng")]
        /// <summary>
        /// Gets or sets a value indicating whether the entity is ship enabled
        /// </summary>
        public bool IsShipEnabled { get; set; }

        [Display(Name = "Giao hàng miển phí")]
        /// <summary>
        /// Gets or sets a value indicating whether the entity is free shipping
        /// </summary>
        public bool IsFreeShipping { get; set; }

        [Display(Name = "Sản phẩm nổi bật")]
        /// <summary>
        /// Gets or sets a value indicating whether the entity is free shipping
        /// </summary>
        public bool IsFeaturedProduct { get; set; }

        [Display(Name = "Phí giao hàng phát sinh")]
        /// <summary>
        /// Gets or sets the additional shipping charge
        /// </summary>
        public Nullable<int> AdditionalShippingCharge { get; set; }

        [Display(Name = "Số lượng hiện có")]
        /// <summary>
        /// Gets or sets the stock quantity
        /// </summary>
        public Nullable<int> StockQuantity { get; set; }

        [Display(Name = "Giá bán")]
        /// <summary>
        /// Gets or sets the price
        /// </summary>
        public decimal Price { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        /// <summary>
        /// Gets or sets a display order. This value is used when sorting associated products (used with "grouped" products)
        /// </summary>
        public Nullable<int> DisplayOrder { get; set; }

        public Nullable<int> ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        

        [Display(Name = "Hiển thị")]
        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        [Display(Name = "Hình đại diện")]
        [DataType(DataType.ImageUrl)]
        public string ImageLink { get; set; }
        //       [Display(Name="H?t hàng")]
        /// <summary>
        /// Gets or sets a value indicating whether the product is available
        /// </summary>
        //     public bool Available { get; set; }
        /// <summary>
        /// Gets or sets the Category
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets the collection of ProductManufacturer
        /// </summary>
        //  public virtual Manufacturer Manufacturer { get; set; }
        //  public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        // public virtual ICollection<Picture> Pictures { get; set; }

        private string _url;

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(_url))
                    _url = GetUrl();
                return _url;
            }
            set { _url = value; }
        }

        private string _vietnamesePrice;

        [NotMapped]
        public string VietnamesePrice
        {
            get
            {
                if (string.IsNullOrEmpty(_vietnamesePrice))
                    _vietnamesePrice = GetVietnamesePirce();
                return _vietnamesePrice;
            }
            set { _vietnamesePrice = value; }
        }

        public string GetVietnamesePirce()
        {
            System.Globalization.CultureInfo vietnam = new CultureInfo(1066);
            CultureInfo usa = new CultureInfo("en-US");

            NumberFormatInfo nfi = usa.NumberFormat;
            nfi = (NumberFormatInfo)nfi.Clone();
            NumberFormatInfo vnfi = vietnam.NumberFormat;
            nfi.CurrencySymbol = vnfi.CurrencySymbol;
            nfi.CurrencyNegativePattern = vnfi.CurrencyNegativePattern;
            nfi.CurrencyPositivePattern = vnfi.CurrencyPositivePattern;
            return this.Price.ToString("c0", nfi);

        }

        public string GetUrl()
        {
            var returnUrl = "";
            if (this.CategoryId.HasValue)
                returnUrl = "/" + this.Category.Slug + "/" + this.Id + "_" + this.Slug;
            else
                returnUrl = "/" + this.Id + "_" + this.Slug;
            return returnUrl;
        }
    }
}