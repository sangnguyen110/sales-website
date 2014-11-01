using System.Collections.Generic;

namespace HoangViet.Models.Catalog
{
    /// <summary>
    /// Represents a picture
    /// </summary>
    public partial class Picture : BaseEntity
    {
        public Picture()
        {
            //Products = new List<Product>();
            //Categories = new List<Category>();
        }
        /// <summary>
        /// Gets or sets the picture binary
        /// </summary>
        public byte[] PictureBinary { get; set; }

        /// <summary>
        /// Gets or sets the picture mime type
        /// </summary>
        public string MimeType { get; set; }

         /// <summary>
        /// Gets or sets the SEO friednly filename of the picture
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the picture is new
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets or sets the product pictures
        /// </summary>
        public virtual Product Products { get; set; }

      //  public virtual ICollection<Category> Categories { get; set; }
    }
}
