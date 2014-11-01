using System;
using System.Collections.Generic;
using HoangViet.Models;
using System.ComponentModel.DataAnnotations;

namespace HoangViet.Models.Catalog
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public partial class Category : BaseEntity
    {
        public Category()
        {
           this.Products = new List<Product>();
           this.ChildCategories = new List<Category>();
          
          
           // this.PageSize = 0;
            this.DisplayOrder = 0;
            this.Published = false;
            this.ShowOnHomePage = false;
        }
        [Required]
        [Display(Name="Tên danh mục hàng hóa")]
     //   [StringLength(100)]
        public string CategoryName { get; set; }

    //    [StringLength(200)]
        public string Slug { get; set; }
        
      //  public string Description { get; set; }
         [Display(Name = "SEO - từ khóa")]
        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }
      [Display(Name = "SEO - mô tả")]
        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }
         [Display(Name = "SEO - tiêu đề")]
        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }
        /// <summary>
        /// Gets or sets the parent category identifier
        /// </summary>
        public Nullable<int> ParentCategoryId { get; set; }
        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
     //   public Nullable<int> PictureId { get; set; }
        /// <summary>
        /// Gets or sets the page size
        /// </summary>
      //  public Nullable<int> PageSize { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether customers can select the page size
        /// </summary>
     //   public bool AllowCustomersToSelectPageSize { get; set; }
        [Display(Name = "Hiển thị trên trang chủ")]
        /// <summary>
        /// Gets or sets a value indicating whether to show the category on home page
        /// </summary>
        public bool ShowOnHomePage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to include this category in the top menu
        /// </summary>
      //  public bool IncludeInTopMenu { get; set; }
          [Display(Name = "Hiển thị")]
        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }
         [Display(Name = "Thứ tự hiển thị")]
        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public Nullable<int> DisplayOrder { get; set; }
        /// /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
      //  public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
      //  public DateTime UpdatedOnUtc { get; set; }
         [Display(Name = "Danh mục cha")]
        /// <summary>
        /// Gets or sets the picture
        /// </summary>
     //   public virtual Picture Picture { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
       public virtual ICollection<Product> Products { get; set; }

       public string GetUrl()
       {
           return "/" + this.Slug;
       }
    }
}
