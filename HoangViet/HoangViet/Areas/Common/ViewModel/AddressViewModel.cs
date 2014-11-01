using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoangViet.Areas.Common.ViewModel
{
    public class AddressViewModel
    {
        public AddressViewModel()
        {
            this.DefaultBillingAddress = false;
            this.DefaultShippingAddress = false;
        }
        [Display(Name="Họ và tên")]
        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public string FullName { get; set; }
       
        /// <summary>
        /// Gets or sets the last name
        /// </summary>
     //   public string LastName { get; set; }

         [Display(Name = "Địa chỉ email")]
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }
        [Display(Name = "Tên công ty")]
        /// <summary>
        /// Gets or sets the company
        /// </summary>
        public string Company { get; set; }

        [Display(Name = "Thành phố")]
        /// <summary>
        /// Gets or sets the city identifier
        /// </summary>
        public int CityId { get; set; }
        [Display(Name = "Quận/Huyện")]
        /// <summary>
        /// Gets or sets the district identifier
        /// </summary>
        public int DistrictId { get; set; }

        [Required(ErrorMessage="Phải nhập địa chỉ",AllowEmptyStrings=false)]
        [Display(Name = "Địa chỉ")]
        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        public string Address1 { get; set; }

        [Display(Name = "Số điện thoại di động")]
        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string PhoneNumber { get; set; }

          [Display(Name = "Số fax")]
        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public string FaxNumber { get; set; }

        [Display(Name = "Địa chỉ hóa đơn mặc định")]
        public bool DefaultBillingAddress { get; set; }

          [Display(Name = "Địa chỉ giao hàng mặc định")]
        public bool DefaultShippingAddress { get; set; }

      public string CustomerId { get; set; }
    }
}