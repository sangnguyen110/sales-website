using FluentValidation;
using HoangViet.Models.Orders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangViet.Areas.Shipping.ViewModels
{
    public class ShipmentValidator : AbstractValidator<ShipmentAdminViewModel>
    {
        public ShipmentValidator()
        {
            RuleFor(x => x.DeliveryDate).GreaterThanOrEqualTo(x => x.ShippedDate);
        }
    }
    [FluentValidation.Attributes.Validator(typeof(ShipmentValidator))]
    public class ShipmentAdminViewModel
    {
        public ShipmentAdminViewModel()
        {
            this.OrderDetails = new List<OrderDetail>();
            this.ShipmentDetailViewModels = new List<ShipmentDetailViewModel>();
        }
        [Display(Name="Mã giao hàng")]
        /// <summary>
        /// Gets or sets the shipment id
        /// </summary>
        public int Id { get; set; }

         [Display(Name = "Mã đơn hàng")]
        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int? OrderId { get; set; }

         [Display(Name = "Mã người giao hàng")]
        /// <summary>
        /// Gets or sets the shipment provider
        /// </summary>
        public int? ShipperId { get; set; }

         [Display(Name = "Tracking number")]
        /// <summary>
        /// Gets or sets the tracking number of this shipment
        /// </summary>
        public string TrackingNumber { get; set; }

         [Display(Name = "Tổng khối lượng")]
        /// <summary>
        /// Gets or sets the total weight of this shipment
        /// It's nullable for compatibility with the previous version of nopCommerce where was no such property
        /// </summary>
        public decimal? TotalWeight { get; set; }

        [Display(Name = "Ngày giao hàng")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
      , ErrorMessage = "Ngày phải theo format dd/MM/yyyy")]
        /// <summary>
        /// Gets or sets the shipped date and time
        /// </summary>
        public string ShippedDate { get; set; }

        [Display(Name = "Ngày nhận hàng")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
      , ErrorMessage = "Ngày phải theo format dd/MM/yyyy")]
        /// <summary>
        /// Gets or sets the delivery date and time
        /// </summary>
        public string DeliveryDate { get; set; }

        [Display(Name = "Giao hàng có VAT")]
        /// <summary>
        /// Gets or sets the order shipping (incl tax)
        /// </summary>
        public decimal ShippingInclTax { get; set; }

        [Display(Name = "Giao hàng chưa VAT")]
        /// <summary>
        /// Gets or sets the order shipping (excl tax)
        /// </summary>
        public decimal ShippingExclTax { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual IList<ShipmentDetailViewModel> ShipmentDetailViewModels { get; set; }
    }
}