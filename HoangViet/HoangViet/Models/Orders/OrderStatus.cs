using System.ComponentModel.DataAnnotations;
namespace HoangViet.Models.Orders
{
    /// <summary>
    /// Represents an order status enumeration
    /// </summary>
    public enum OrderStatus : int
    {
        [Display(Name="Chưa xử lý")]
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,
         [Display(Name = "Đang xử lý")]
        /// <summary>
        /// Processing
        /// </summary>
        Processing = 20,
         [Display(Name = "Hoàn tất")]
        /// <summary>
        /// Complete
        /// </summary>
        Complete = 30,
         [Display(Name = "Đã hủy")]
        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 40
    }
}
