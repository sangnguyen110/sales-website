using HoangViet.Models.Catalog;
using HoangViet.Models.Shipping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangViet.Models.Orders
{
    /// <summary>
    /// Represents an order item
    /// </summary>
    public partial class OrderDetail : BaseEntity
    {
        public OrderDetail()
        {
            this.ShipmentDetails = new List<ShipmentDetail>();
        }
     
        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn hoặc bằng {1}")]
        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }


        /// <summary>
        /// Gets or sets the unit price in primary store currency (incl tax)
        /// </summary>
        public decimal UnitPriceInclTax { get; set; }

        /// <summary>
        /// Gets or sets the unit price in primary store currency (excl tax)
        /// </summary>
        public decimal UnitPriceExclTax { get; set; }

        /// <summary>
        /// Gets or sets the price in primary store currency (incl tax)
        /// </summary>
        public decimal PriceInclTax { get; set; }

        /// <summary>
        /// Gets or sets the price in primary store currency (excl tax)
        /// </summary>
        public decimal PriceExclTax { get; set; }

        /// <summary>
        /// Gets or sets the original cost of this order item (when an order was placed), qty 1
        /// </summary>
       // public decimal OriginalProductCost { get; set; }

        /// <summary>
        /// Gets or sets the total weight of one item
        /// It's nullable for compatibility with the previous version of nopCommerce where was no such property
        /// </summary>
      //  public decimal? ItemWeight { get; set; }

        /// <summary>
        /// Gets the order
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Product Product { get; set; }

        public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; }

        public void CalOrderDetailAmounts()
        {
            this.PriceExclTax = this.UnitPriceExclTax * this.Quantity;
        }

    }
}
