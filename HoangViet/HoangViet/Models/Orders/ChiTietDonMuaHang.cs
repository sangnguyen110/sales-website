using HoangViet.Models.Catalog;
using System;
using System.Collections.Generic;

namespace HoangViet.Models.Orders
{
    /// <summary>
    /// Represents an order item
    /// </summary>
    public partial class ChiTietDonMuaHang : BaseEntity
    {  
        
        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int DonHangId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int HangHoaId { get; set; }

        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int SoLuong { get; set; }

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
        public decimal OriginalProductCost { get; set; }

        /// <summary>
        /// Gets the order
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Product Product { get; set; }

    }
}
