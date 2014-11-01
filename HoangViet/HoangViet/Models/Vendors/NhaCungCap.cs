
namespace HoangViet.Models.Vendors
{
    /// <summary>
    /// Represents a vendor
    /// </summary>
    public partial class NhaCungCap : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Ten { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string MoTa { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }


    }
}
