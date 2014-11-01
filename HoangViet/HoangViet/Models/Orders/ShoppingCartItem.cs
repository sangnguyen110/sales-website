using HoangViet.Models.Accounts;
using HoangViet.Models.Catalog;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace HoangViet.Models.Orders
{
    /// <summary>
    /// Represents a shopping cart item
    /// </summary>
    public partial class ShoppingCartItem : BaseEntity
    {

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }


        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        [NotMapped]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
   //     public DateTime UpdatedOnUtc { get; set; }
        
        /// <summary>
        /// Gets the log type
        /// </summary>
        public ShoppingCartType ShoppingCartType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Product Product { get; set; }

        private string _total;
        [NotMapped]
        public string Total
        {
            get
            {
                if (string.IsNullOrEmpty(_total))
                    _total = GetVietnamesePirce();
                return _total;
            }
            set { _total = value; }
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
            return (this.Product.Price*this.Quantity).ToString("c0", nfi);

        }
        /// <summary>
        /// Gets or sets the customer
        /// </summary>
      //  public virtual HoangVietUser Customer { get; set; }

        /// <summary>
        /// Gets a value indicating whether the shopping cart item is free shipping
        /// </summary>
        //public bool IsFreeShipping
        //{
        //    get
        //    {
        //        var product = this.Product;
        //        if (product != null)
        //            return product.IsFreeShipping;
        //        return true;
        //    }
        //}

        /// <summary>
        /// Gets a value indicating whether the shopping cart item is ship enabled
        /// </summary>
        //public bool IsShipEnabled
        //{
        //    get
        //    {
        //        var product = this.Product;
        //        if (product != null)
        //            return product.IsShipEnabled;
        //        return false;
        //    }
        //}

       
       
    }
}
