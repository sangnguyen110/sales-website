using HoangViet.Models.Common;
using HoangViet.Models.Payments;
using HoangViet.Models.Shipping;
using HoangViet.Models.Vendors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace HoangViet.Models.Orders
{
    /// <summary>
    /// Represents an order
    /// </summary>
    public partial class DonMuaHang : BaseEntity
    {

        private ICollection<GhiChuDonMuaHang> _orderNotes;
        private ICollection<ChiTietDonMuaHang> _orderItems;
     

        #region Utilities


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets an order status identifier
        /// </summary>
        public int TrangThaiDonHangId { get; set; }

        /// <summary>
        /// Gets or sets the shipping status identifier
        /// </summary>
        public int TrangThaiGiaoHangId { get; set; }

        /// <summary>
        /// Gets or sets the payment status identifier
        /// </summary>
        public int TrangThaiThanhToanId { get; set; }
   
        /// <summary>
        /// Gets or sets the order subtotal (incl tax)
        /// </summary>
        public decimal OrderSubtotalInclTax { get; set; }

        /// <summary>
        /// Gets or sets the order subtotal (excl tax)
        /// </summary>
        public decimal OrderSubtotalExclTax { get; set; }

        /// <summary>
        /// Gets or sets the order shipping (incl tax)
        /// </summary>
        public decimal OrderShippingInclTax { get; set; }

        /// <summary>
        /// Gets or sets the order shipping (excl tax)
        /// </summary>
        public decimal OrderShippingExclTax { get; set; }


        /// <summary>
        /// Gets or sets the tax rates
        /// </summary>
        public string TaxRates { get; set; }

        /// <summary>
        /// Gets or sets the order tax
        /// </summary>
        public decimal OrderTax { get; set; }

        /// <summary>
        /// Gets or sets the order total
        /// </summary>
        public decimal OrderTotal { get; set; }
        /// <summary>
        /// Gets or sets the NgayMua
        /// </summary>
        public DateTime? NgayMua { get; set; }
        /// <summary>
        /// Gets or sets the NgayNhanHang
        /// </summary>
        public DateTime? NgayNhanHang { get; set; }
        /// <summary>
        /// Gets or sets the SoHoaDon
        /// </summary>
        public string SoHoaDon { get; set; }
        /// <summary>
        /// Gets or sets the NgayHoaDon
        /// </summary>
        public DateTime? NgayHoaDon { get; set; }
        /// <summary>
        /// Gets or sets the paid date and time
        /// </summary>
        public DateTime? NgayNhanHoaDon { get; set; }
    
        /// <summary>
        /// Gets or sets the paid date and time
        /// </summary>
        public DateTime? NgayThanhToan { get; set; }
        
     
        /// <summary>
        /// Gets or sets the shipping method
        /// </summary>
        public string ShippingMethod { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of order creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        #endregion

        #region Navigation properties

        /// <summary>
        /// Gets or sets the NhaCungCap
        /// </summary>
        public virtual NhaCungCap NhaCungCap { get; set; }

        /// <summary>
        /// Gets or sets order notes
        /// </summary>
        public virtual ICollection<GhiChuDonMuaHang> OrderNotes
        {
            get { return _orderNotes ?? (_orderNotes = new List<GhiChuDonMuaHang>()); }
            protected set { _orderNotes = value; }
        }

        /// <summary>
        /// Gets or sets order items
        /// </summary>
        public virtual ICollection<ChiTietDonMuaHang> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new List<ChiTietDonMuaHang>()); }
            protected set { _orderItems = value; }
        }


        #endregion

        #region Custom properties

        /// <summary>
        /// Gets or sets the order status
        /// </summary>
        public OrderStatus OrderStatus
        {
            get
            {
                return (OrderStatus)this.TrangThaiDonHangId;
            }
            set
            {
                this.TrangThaiDonHangId = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the payment status
        /// </summary>
        public PaymentStatus PaymentStatus
        {
            get
            {
                return (PaymentStatus)this.TrangThaiThanhToanId;
            }
            set
            {
                this.TrangThaiThanhToanId = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the shipping status
        /// </summary>
        public ShippingStatus ShippingStatus
        {
            get
            {
                return (ShippingStatus)this.TrangThaiGiaoHangId;
            }
            set
            {
                this.TrangThaiGiaoHangId = (int)value;
            }
        }
        
        #endregion
    }
}
