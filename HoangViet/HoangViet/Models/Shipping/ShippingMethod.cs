using HoangViet.Models.Accounts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangViet.Models.Shipping
{
    /// <summary>
    /// Represents a shipping method (used for offline shipping rate computation methods)
    /// </summary>
    public partial class ShippingMethod : BaseEntity
    {
        public ShippingMethod()
        {
            this.Shipments = new List<Shipment>();
        }
        [Display(Name="Tên phương thức giao hàng")]
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string ShippingMethodName { get; set; }
        [Display(Name="Mô tả")]
        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }
        [Display(Name = "Tên người cần liên hệ")]
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string ContactName { get; set; }
        [Display(Name="Giới tính")]
        /// <summary>
        /// Gets or sets a value 'Gender' 
        /// </summary>
        public Gender Sex { get; set; }
        [Display(Name="Số điện thoại liên hệ")]
        public string Phone { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}