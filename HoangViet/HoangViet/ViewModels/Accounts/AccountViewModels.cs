using HoangViet.Models.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HoangViet.ViewModels.Accounts
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Lưu tài khoản")]
        public bool RememberMe { get; set; }


    }

    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [EmailAddress(ErrorMessage = "Địa chỉ email đã nhập không đúng")]
        [Display(Name = "Địa chỉ email *")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [StringLength(100, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Mật khẩu đã nhập không hợp lệ")]
        [Display(Name = "Mật khẩu *")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu *")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu xác nhận không trùng khớp")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [Display(Name = "Họ và Tên *")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [Display(Name = "Giới tính *")]
        //  [EnumDataType(typeof(Gender), ErrorMessage = "Thông tin không hợp lệ")]
        public Gender Sex { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [Display(Name = "Ngày sinh *")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
      , ErrorMessage = "Ngày sinh phải theo format dd/MM/yyyy")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Tên công ty")]
        public string CompanyName { get; set; }
        [Display(Name = "Số Fax")]
        [StringLength(8, ErrorMessage = "Thông tin đã nhập không hợp lệ")]
        public string FaxNumber { get; set; }

        [Display(Name = "Mã số thuế")]
        public string TaxCode { get; set; }

        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ")]
        //  [RegularExpression("#^\(?[\d]{3}\)?-\(?[\d]{2}\)?-[\d]{2}\.[\d]{3}-[\d]{3}$#", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ website")]
        [Url(ErrorMessage = "Địa chỉ web không hợp lệ")]
        public string Website { get; set; }

        [Display(Name = "Khóa tài khoản")]
        public bool Disabled { get; set; }

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

        [Display(Name = "Vai trò")]
        public IEnumerable<SelectListItem> RolesList { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Địa chỉ Email của bạn")]
        public string Email { get; set; }
    }
}