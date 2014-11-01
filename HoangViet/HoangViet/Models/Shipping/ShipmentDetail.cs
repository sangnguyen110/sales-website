
using HoangViet.Models.Orders;
namespace HoangViet.Models.Shipping
{
    /// <summary>
    /// Represents a shipment item
    /// </summary>
    public partial class ShipmentDetail : BaseEntity
    {
        /// <summary>
        /// Gets or sets the shipment identifier
        /// </summary>
        public int ShipmentId { get; set; }

        /// <summary>
        /// Gets or sets the order item identifier
        /// </summary>
        public int OrderDetailId { get; set; }

        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the shipment
        /// </summary>
        public virtual Shipment Shipment { get; set; }

        /// <summary>
        /// Gets the shipment
        /// </summary>
        public virtual OrderDetail OrderDetail { get; set; }
    }
}