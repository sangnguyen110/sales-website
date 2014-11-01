using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangViet.Models.Common
{
    /// <summary>
    /// Represents a country
    /// </summary>
    public partial class City : BaseEntity
    {
        private ICollection<District> _districts;
        public City()
        {
            Published = false;
            this.Addresses = new List<Address>();
        }

        [Display(Name="Thành phố")]
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string CityName { get; set; }


        /// <summary>
        /// Gets or sets the two letter ISO code
        /// </summary>
       // public string TwoLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the three letter ISO code
        /// </summary>
       // public string ThreeLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the numeric ISO code
        /// </summary>
       // public int NumericIsoCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

       
        /// <summary>
        /// Gets or sets the state/provinces
        /// </summary>
        public virtual ICollection<District> Districts
        {
            get { return _districts ?? (_districts = new List<District>()); }
            protected set { _districts = value; }
        }
        public virtual ICollection<Address> Addresses { get; set; }

    }

}
