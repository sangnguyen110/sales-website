
namespace HoangViet.Models.Tax
{
    /// <summary>
    /// Represents a tax category
    /// </summary>
    public partial class TaxCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Ten { get; set; }
        /// <summary>
        /// Gets or sets the percentage
        /// </summary>
        public decimal Percentage { get; set; }
        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
    }

}
