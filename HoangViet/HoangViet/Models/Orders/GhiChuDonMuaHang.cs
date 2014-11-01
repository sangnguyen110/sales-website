using System;

namespace HoangViet.Models.Orders
{
    /// <summary>
    /// Represents an order note
    /// </summary>
    public partial class GhiChuDonMuaHang : BaseEntity
    {
        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int DonHangId { get; set; }

        /// <summary>
        /// Gets or sets the note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the date and time of order note creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets the order
        /// </summary>
        public virtual DonMuaHang Order { get; set; }
    }

}
