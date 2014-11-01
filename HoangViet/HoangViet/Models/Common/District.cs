

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace HoangViet.Models.Common
{
    /// <summary>
    /// Represents a state/province
    /// </summary>
    public partial class District : BaseEntity
    {
        public District()
        {
            this.Published = false;
            this.Addresses = new List<Address>();
        }

          [Display(Name = "Quận/Huyện")]
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation
        /// </summary>
     //   public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
       public int DisplayOrder { get; set; }
       /// <summary>
       /// Gets or sets the city identifier
       /// </summary>
       public int? CityId { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public virtual City City { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }

}
