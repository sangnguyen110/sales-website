using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoangViet.Areas.Shipping.ViewModels
{
    public class ShipmentDetailValidator : AbstractValidator<ShipmentDetailViewModel>
    {
        public ShipmentDetailValidator()
        {
            RuleFor(x => x.ShipmentDetailQuantity).LessThanOrEqualTo(x => x.DefaultShipmentDetailQuantity);
        }
    }
      [FluentValidation.Attributes.Validator(typeof(ShipmentDetailValidator))]
    public class ShipmentDetailViewModel
    {
        /// <summary>
        /// Gets or sets the ShipmentDetail identifier
        /// </summary>
        public int ShipmentDetailId { get; set; }

        /// <summary>
        /// Gets or sets the order item identifier
        /// </summary>
        public int OrderDetailId { get; set; }

        /// <summary>
        /// Gets or sets the OrderDetail quantity
        /// </summary>
        public int OrderDetailQuantity { get; set; }

        /// <summary>
        /// Gets or sets the Ship quantity
        /// </summary>
        public int ShippedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the default enter quantity
        /// </summary>
        public int DefaultShipmentDetailQuantity { get; set; }

          [Range(1, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn hoặc bằng {1}")]
        /// <summary>
        /// Gets or sets the enter quantity
        /// </summary>
        public int ShipmentDetailQuantity { get; set; }

        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string ProductName { get; set; }
    }
}