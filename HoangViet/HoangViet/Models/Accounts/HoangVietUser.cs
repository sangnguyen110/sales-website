using HoangViet.Models.Common;
using HoangViet.Models.Orders;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HoangViet.Models.Accounts
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class HoangVietUser : IdentityUser
    {
        public HoangVietUser()
        {
            this.Orders = new List<Order>();
            this.Addresses = new List<Address>();
        }
       // private ICollection<ChiTietGioHang> _shoppingCartItems;
      //  private ICollection<Address> _addresses;
        /// <summary>
        /// Gets or sets ten
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value avatars.
        /// </summary>
     //   public string Avatar { get; set; }
        /// <summary>
        /// Gets or sets a value 'Gender' 
        /// </summary>
        public Gender Sex { get; set; }
        /// <summary>
        /// Gets or sets a value 'Date of Birth' 
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets a value 'Company'
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Gets or sets a value 'Fax number'
        /// </summary>
        public string FaxNumber { get; set; }
        /// <summary>
        /// Gets or sets a value 'VATNumber' 
        /// </summary>
        public string TaxCode { get; set; }
        public string Website { get; set; }
        //public int? BillingAddressId { get; set; }
        //public int? ShippingAddressId { get; set; }
        public bool Disabled { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<HoangVietUser> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [NotMapped]
        public string RolesName { get; set; }
        public async Task<string> GetRolesNameAsync(UserManager<HoangVietUser> manager)
        {
            var rolesName = await manager.GetRolesAsync(this.Id);
            return string.Join(", ",rolesName);
        }
        //[NotMapped]
        //public ObjectState ObjectState { get; set; }
        #region Navigation properties

        /// <summary>
        /// Gets or sets shopping cart items
        /// </summary>
        //public virtual ICollection<ChiTietGioHang> ShoppingCartItems
        //{
        //    get { return _shoppingCartItems ?? (_shoppingCartItems = new List<ChiTietGioHang>()); }
        //    protected set { _shoppingCartItems = value; }
        //}

        /// <summary>
        /// Default billing address
        /// </summary>
      //  public virtual Address BillingAddress { get; set; }

        /// <summary>
        /// Default shipping address
        /// </summary>
      // public virtual Address ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets customer addresses
        /// </summary>
        //public virtual ICollection<Address> Addresses
        //{
        //    get { return _addresses ?? (_addresses = new List<Address>()); }
        //    protected set { _addresses = value; }
        //}
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }


        #endregion
    }
    public enum Gender:int
    {
        [Display(Name = "Nam")]
        Male = 10,
        [Display(Name = "Nữ")]
        Female = 20,
        [Display(Name = "Giới tính khác")]
        Other = 30

    }
   
}