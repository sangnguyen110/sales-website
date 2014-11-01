using HoangViet.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangViet.Models.Shipping
{
    /// <summary>
    /// Represents a shipment
    /// </summary>
    public partial class Shipment : BaseEntity
    {
        public Shipment()
        {
            this.ShipmentDetails = new List<ShipmentDetail>();
        }

        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the shipment provider
        /// </summary>
        public int? ShipperId { get; set; }
        
        /// <summary>
        /// Gets or sets the tracking number of this shipment
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Gets or sets the total weight of this shipment
        /// It's nullable for compatibility with the previous version of nopCommerce where was no such property
        /// </summary>
        public decimal? TotalWeight { get; set; }

        /// <summary>
        /// Gets or sets the shipped date and time
        /// </summary>
        public DateTime? ShippedDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the delivery date and time
        /// </summary>
        public DateTime? DeliveryDateUtc { get; set; }

        [Display(Name = "Giao hàng có VAT")]
        /// <summary>
        /// Gets or sets the order shipping (incl tax)
        /// </summary>
        public decimal ShippingInclTax { get; set; }

        [Display(Name = "Giao hàng chýa VAT")]
        /// <summary>
        /// Gets or sets the order shipping (excl tax)
        /// </summary>
        public decimal ShippingExclTax { get; set; }

        /// <summary>
        /// Gets or sets the entity creation date
        /// </summary>
      //  public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets the order
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets the shipper
        /// </summary>
        public virtual ShippingMethod Shipper { get; set; }

        /// <summary>
        /// Gets or sets the shipment details
        /// </summary>
        public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; }
    }
}