using System.ComponentModel.DataAnnotations;
namespace HoangViet.Models.Shipping
{
    /// <summary>
    /// Represents the shipping status enumeration
    /// </summary>
    public enum ShippingStatus : int
    {
         [Display(Name = "Không giao hàng")]
        /// <summary>
        /// Shipping not required
        /// </summary>
        ShippingNotRequired = 10,
         [Display(Name = "Chưa giao hàng")]
        /// <summary>
        /// Not yet shipped
        /// </summary>
        NotYetShipped = 20,
         [Display(Name = "Đã giao một phần")]
        /// <summary>
        /// Partially shipped
        /// </summary>
        PartiallyShipped = 25,
         [Display(Name = "Đã giao")]
        /// <summary>
        /// Shipped
        /// </summary>
        Shipped = 30,
         [Display(Name = "Đã nhận hàng")]
        /// <summary>
        /// Delivered
        /// </summary>
        Delivered = 40,
    }
}
