using HoangViet.Models.Catalog;
using System;
using System.Collections.Generic;

namespace HoangViet.Models.Bids
{
    public class Listing:BaseEntity
    {
        private Product _product;
        private ICollection<Bid> _bids;
        public int ProductId { get; set; }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Mota
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the gia khoi diem
        /// </summary>
        public decimal? StartingPrice { get; set; }
    
        /// <summary>
        /// Gets or sets the buoc gia
        /// </summary>
        public decimal? Increment { get; set; }
        /// <summary>
        /// Gets or sets the GiaThangCuoc
        /// </summary>
        public decimal? WinningPrice { get; set; }
        /// <summary>
        /// Gets or sets the current highest price
        /// </summary>
        public decimal? HighestPrice { get; set; }
        /// <summary>
        /// Gets or sets the so luong hang hoa
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the so luong hang hoa
        /// </summary>
        public int CountBid { get; set; }
        /// <summary>
        /// Gets or sets the thoi gian bat dau
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Gets or sets the thoi gian ket thuc
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        public virtual Product Product
        {
            get { return _product ?? (_product = new Product()); }
            protected set { _product = value; }
        }

        public virtual ICollection<Bid> Bids
        {
            get { return _bids ?? (_bids = new List<Bid>()); }
            protected set { _bids = value; }
        }
    }
}