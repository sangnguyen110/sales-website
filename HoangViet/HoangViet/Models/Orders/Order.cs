using HoangViet.Models.Accounts;
using HoangViet.Models.Common;
using HoangViet.Models.Payments;
using HoangViet.Models.Shipping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace HoangViet.Models.Orders
{
    /// <summary>
    /// Represents an order
    /// </summary>
    public partial class Order : BaseEntity
    {
        public Order()
        {
            this.Shipments = new List<Shipment>();
            this.OrderDetails = new List<OrderDetail>();
        }

        #region Utilities


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public string CustomerId { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH':'mm':'ss}")]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [Display(Name = "Địa chỉ hóa đơn")]
        /// <summary>
        /// Gets or sets the billing address identifier
        /// </summary>
        public int? BillingAddressId { get; set; }

        [Display(Name = "Địa chỉ giao hàng")]
        /// <summary>
        /// Gets or sets the shipping address identifier
        /// </summary>
        public int? ShippingAddressId { get; set; }

        [Display(Name = "Trạng thái đơn đặt hàng")]
        /// <summary>
        /// Gets or sets an order status identifier
        /// </summary>
        public OrderStatus OrderStatus { get; set; }
        [Display(Name = "Trạng thái giao hàng")]
        /// <summary>
        /// Gets or sets the shipping status identifier
        /// </summary>
        public ShippingStatus ShippingStatus { get; set; }
        [Display(Name = "Trạng thái thanh toán")]
        /// <summary>
        /// Gets or sets the payment status identifier
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }
        [Display(Name = "Phương thức thanh toán")]
        /// <summary>
        /// Gets or sets the payment method system name
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        // [Display(Name = "Mã số thuế của khách hàng")]
        /// <summary>
        /// Gets or sets the VAT number (the European Union Value Added Tax)
        /// </summary>
        //  public string VatNumber { get; set; }

        [Display(Name = "Đơn hàng có VAT")]
        /// <summary>
        /// Gets or sets the order subtotal (incl tax)
        /// </summary>
        public decimal OrderSubtotalInclTax { get; set; }
        [Display(Name = "Đơn hàng chưa VAT")]
        /// <summary>
        /// Gets or sets the order subtotal (excl tax)
        /// </summary>
        public decimal OrderSubtotalExclTax { get; set; }
        [Display(Name = "Giao hàng có VAT")]
        /// <summary>
        /// Gets or sets the order shipping (incl tax)
        /// </summary>
        public decimal OrderShippingInclTax { get; set; }

        [Display(Name = "Giao hàng chưa VAT")]
        /// <summary>
        /// Gets or sets the order shipping (excl tax)
        /// </summary>
        public decimal OrderShippingExclTax { get; set; }

        [Display(Name = "Thuế suất")]
        /// <summary>
        /// Gets or sets the tax rates
        /// </summary>
        public decimal TaxRates { get; set; }

        /// <summary>
        /// Gets or sets the order tax
        /// </summary>
        public decimal OrderTax { get; set; }



        [Display(Name = "Tổng")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        /// <summary>
        /// Gets or sets the order total
        /// </summary>
        public decimal OrderTotal { get; set; }


        /// <summary>
        /// Gets or sets the purchase order number
        /// </summary>
        //public string PurchaseOrderNumber { get; set; }

        [Display(Name = "Ngày thanh toán")]
        /// <summary>
        /// Gets or sets the paid date and time
        /// </summary>
        public DateTime? PaidDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the shipping method
        /// </summary>
        //        public string ShippingMethod { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        //  public bool Deleted { get; set; }
        //  [Display(Name = "Ngày lập")]
        /// <summary>
        /// Gets or sets the date and time of order creation
        /// </summary>
        // public DateTime CreatedOnUtc { get; set; }
        [Display(Name = "Ngày thanh toán")]
        [NotMapped]
        public string PaidDate { get; set; }

        #endregion

        #region Navigation properties
        [Display(Name = "Email của khách hàng")]
        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public virtual HoangVietUser Customer { get; set; }

        [Display(Name = "Địa chỉ hóa đơn")]
        /// <summary>
        /// Gets or sets the billing address
        /// </summary>
        public virtual Address BillingAddress { get; set; }

        [Display(Name = "Địa chỉ giao hàng")]
        /// <summary>
        /// Gets or sets the shipping address
        /// </summary>
        public virtual Address ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets order notes
        /// </summary>
        //public virtual ICollection<GhiChuDonBanHang> OrderNotes
        //{
        //    get { return _orderNotes ?? (_orderNotes = new List<GhiChuDonBanHang>()); }
        //    protected set { _orderNotes = value; }
        //}

        /// <summary>
        /// Gets or sets order items
        /// </summary>
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets shipments
        /// </summary>
        public virtual ICollection<Shipment> Shipments { get; set; }

        #endregion

        public void CalOrderAmount()
        {
            this.OrderSubtotalExclTax = 0;
            foreach (var orderDetail in this.OrderDetails)
                this.OrderSubtotalExclTax += orderDetail.PriceExclTax;

            this.OrderTax = this.OrderSubtotalExclTax * this.TaxRates / 100;

            this.OrderSubtotalInclTax = this.OrderSubtotalExclTax + this.OrderTax;

            this.OrderShippingExclTax = 0;
            this.OrderShippingInclTax = 0;
            foreach (var shipment in this.Shipments)
            {
                this.OrderShippingExclTax += shipment.ShippingExclTax;
                this.OrderShippingInclTax += shipment.ShippingInclTax;
            }

            this.OrderTotal = this.OrderSubtotalInclTax + this.OrderShippingInclTax;

        }

        public bool EnabledShipmentCreating()
        {
            foreach (var orderDetail in this.OrderDetails)
            {
                var shippedQuantity = 0;
                foreach (var shipmentDetail in orderDetail.ShipmentDetails)
                    shippedQuantity += shipmentDetail.Quantity;

                if (orderDetail.Quantity != shippedQuantity)
                    return true;

            }
            return false;

        }

        public void UpdateOrderStatus()
        {
            if (this.PaymentStatus == PaymentStatus.Paid && this.ShippingStatus == ShippingStatus.Delivered
                || this.PaymentStatus == PaymentStatus.Paid && this.ShippingStatus == ShippingStatus.ShippingNotRequired)
                this.OrderStatus = OrderStatus.Complete;
            else
                this.OrderStatus = OrderStatus.Processing;
        }

        public void UpdateShippingStatus()
        {
            if (this.Shipments.Count == 0)
                this.ShippingStatus = Shipping.ShippingStatus.NotYetShipped;
            else
                this.ShippingStatus = Shipping.ShippingStatus.PartiallyShipped;

            var shipmentDetails = new List<ShipmentDetail>();
            foreach (var sm in this.Shipments)
                shipmentDetails.AddRange(sm.ShipmentDetails);

            var shippedOrderDetails = new List<OrderDetail>();
            shippedOrderDetails = shipmentDetails.GroupBy(sd => sd.OrderDetailId).Select(g => new OrderDetail
            {
                Id = g.Key,
                Quantity = g.Sum(sd => sd.Quantity)
            }).ToList();
            var isShipped = true;
            foreach (var orderDetail in this.OrderDetails)
            {
                var temp = shippedOrderDetails.SingleOrDefault(so => so.Id == orderDetail.Id);
                if (temp == null || temp.Quantity != orderDetail.Quantity)
                {
                    isShipped = false;
                    break;
                }
            };
            if (isShipped)
                if (this.Shipments.Any(s => s.DeliveryDateUtc == null))
                    this.ShippingStatus = Shipping.ShippingStatus.Shipped;
                else
                    this.ShippingStatus = Shipping.ShippingStatus.Delivered;
        }
    }
}
