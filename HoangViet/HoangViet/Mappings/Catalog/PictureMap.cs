using HoangViet.Models.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace HoangViet.Mappings.Catalog
{
    public partial class PictureMap : EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
         
         
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(p => p.PictureBinary).IsMaxLength();
            this.Property(p => p.MimeType).IsRequired().HasMaxLength(40);
            this.Property(p => p.SeoFilename).HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("HinhAnh");
            this.Property(t => t.Id).HasColumnName("HinhAnhId");
            this.Property(t => t.IsNew).HasColumnName("Moi");
            this.Property(t => t.MimeType).HasColumnName("Duoi");
            this.Property(t => t.PictureBinary).HasColumnName("HinhAnhBinary");
            this.Property(t => t.SeoFilename).HasColumnName("TenSeo");

            // Relationships
            //this.HasMany(t => t.Products).WithMany(t => t.Pictures).Map(t => t.ToTable("HangHoaHinhAnh").MapLeftKey("HangHoaId").MapRightKey("HinhAnhId"));
            //this.HasMany(t => t.Categories).WithOptional(t => t.Picture).HasForeignKey(t=> t.PictureId);
            //Store procedures
            this.MapToStoredProcedures();
        }
    }
}