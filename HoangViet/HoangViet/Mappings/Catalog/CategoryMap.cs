using HoangViet.Models.Catalog;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HoangViet.Mappings.Catalog
{
    public partial class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(100);
            this.Property(c => c.MetaKeywords).HasMaxLength(150);
            this.Property(c => c.MetaTitle).HasMaxLength(80);
            this.Property(c => c.MetaDescription).HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DanhMucHangHoa");
            this.Property(t => t.Id).HasColumnName("DanhMucHangHoaId");
            this.Property(t => t.CategoryName).HasColumnName("TenDanhMucHangHoa");
            this.Property(t => t.Published).HasColumnName("HienThi");
            this.Property(t => t.ParentCategoryId).HasColumnName("DanhMucChaId");
            this.Property(t => t.DisplayOrder).HasColumnName("ThuTuHienThi");
            this.Property(t => t.Slug).HasColumnName("Slug");
            this.Property(c => c.ShowOnHomePage).HasColumnName("HienThiTrenTrangChu");
        
          //  this.Property(t => t.Description).HasColumnName("MoTa");

            // Relationships
            //this.HasOptional(t => t.ParentCategory).WithMany(t => t.ChildCategories).HasForeignKey(d => d.ParentCategoryId);
            //this.HasOptional(t => t.Picture).WithMany(t => t.Categories).HasForeignKey(d => d.PictureId);
            //this.HasMany(t => t.Products).WithOptional(t => t.Category).HasForeignKey(d => d.CategoryId);
            //Store procedures
           this.MapToStoredProcedures();
        }
    }
}