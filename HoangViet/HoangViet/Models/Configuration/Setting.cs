namespace HoangViet.Models.Configuration
{
    /// <summary>
    /// Represents a setting
    /// </summary>
    public partial class Setting : BaseEntity
    {
        public Setting() { }
        
        public Setting(string name, string value) {
            this.Name = name;
            this.Value = value;
        }
       
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }
     
        public string InputType { get; set; }
        }


}
