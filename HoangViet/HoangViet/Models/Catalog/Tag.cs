using System.Collections.Generic;

namespace HoangViet.Models.Catalog
{
    /// <summary>
    /// Represents a product tag
    /// </summary>
    public partial class Tag : BaseEntity
    {
        private ICollection<Product> _products;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the products
        /// </summary>
        public virtual ICollection<Product> Products
        {
            get { return _products ?? (_products = new List<Product>()); }
            protected set { _products = value; }
        }
    }
}
