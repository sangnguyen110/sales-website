using HoangViet.Models.Accounts;
using System;
using System.ComponentModel.DataAnnotations;

namespace HoangViet.Models.Common
{
    public class Address : BaseEntity
    {
        public Address()
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
        public int? CityId { get; set; }

          [Display(Name = "Quận/Huyện")]
        /// <summary>
        /// Gets or sets the district identifier
        /// </summary>
        public int? DistrictId { get; set; }

          [Display(Name = "Địa chỉ")]
        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
       // public string Address2 { get; set; }
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

        public bool DefaultBillingAddress { get; set; }
        public bool DefaultShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        //public DateTime CreatedOnUtc { get; set; }
        
        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public virtual City City { get; set; }

        /// <summary>
        /// Gets or sets the district
        /// </summary>
        public virtual District District { get; set; }
        public string CustomerId { get; set; }
        public virtual HoangVietUser Customer { get; set; }

        public object Clone()
        {
            var addr = new Address()
            {
                FullName = this.FullName,
               // LastName = this.LastName,
                Email = this.Email,
                Company = this.Company,
                City = this.City,
                CityId = this.CityId,
                DistrictId = this.DistrictId,
                District = this.District,
                Address1 = this.Address1,
               // Address2 = this.Address2,
                PhoneNumber = this.PhoneNumber,
                FaxNumber = this.FaxNumber,
                //CreatedOnUtc = this.CreatedOnUtc,
            };
            return addr;
        }
    }
}
