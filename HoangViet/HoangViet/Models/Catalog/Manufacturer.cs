using System;
using HoangViet.Models;
using System.Collections.Generic;

namespace HoangViet.Models.Catalog
{
    /// <summary>
    /// Represents a manufacturer
    /// </summary>
    public partial class Manufacturer : BaseEntity
    {
        private ICollection<Product> _products;
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }     

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
      //  public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers can select the page size
        /// </summary>
      //  public bool AllowCustomersToSelectPageSize { get; set; }

        /// <summary>
        /// Gets or sets the available customer selectable page size options
        /// </summary>
     //   public string PageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets the available price ranges
        /// </summary>
        public string PriceRanges { get; set; }
      
        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return _products ?? (_products = new List<Product>()); }
            protected set { _products = value; }
        }
    }
}
