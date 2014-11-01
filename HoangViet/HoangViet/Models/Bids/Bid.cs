using System;

namespace HoangViet.Models.Bids
{
    public partial class Bid:BaseEntity
    {
        /// <summary>
        /// Gets or sets the PhienDauGiaID
        /// </summary>
        public int ListingID { get; set; }
        /// <summary>
        /// Gets or sets the gia khoi diem
        /// </summary>
        public decimal? Price { get; set; }       
        /// <summary>
        /// Gets or sets the ThoiGianDatGia
        /// </summary>
        public DateTime? TimeBid { get; set; }
        public virtual Listing Listing { get; set; }
    }
}