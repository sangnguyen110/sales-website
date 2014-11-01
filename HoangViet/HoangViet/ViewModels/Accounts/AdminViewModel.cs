using HoangViet.Models.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HoangViet.ViewModels.Accounts
{
    public class RoleViewModel
    {
         [Display(Name = "Mã role")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Tên role")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [EmailAddress(ErrorMessage = "Địa chỉ email đã nhập không đúng")]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [Display(Name = "Họ và Tên")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [Display(Name = "Giới tính")]
        //  [EnumDataType(typeof(Gender), ErrorMessage = "Thông tin không hợp lệ")]
        public Gender Sex { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập thông tin này")]
        [Display(Name = "Ngày sinh")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
        ,ErrorMessage="Ngày sinh phải theo format dd/MM/yyyy")]
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
        public string PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ website")]
        [Url(ErrorMessage = "Địa chỉ web không hợp lệ")]
        public string Website { get; set; }

        [Display(Name = "Khóa tài khoản")]
        public bool Disabled { get; set; }

         [Display(Name = "Vai trò")]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}