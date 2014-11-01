using HoangViet.Models.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace HoangViet.Mappings.Catalog
{
    public partial class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
             // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);
            this.Property(t => t.ShortDescription).IsUnicode().IsRequired().HasMaxLength(300);
            this.Property(p => p.MetaKeywords).HasMaxLength(150);
            this.Property(p => p.MetaTitle).HasMaxLength(80);
             this.Property(c => c.MetaDescription).HasMaxLength(100);
        //    this.Property(p => p.ManufacturerPartNumber).HasMaxLength(400);
            

            // Table & Column Mappings
            this.ToTable("HangHoa");
            this.Property(t => t.Id).HasColumnName("HangHoaId");
            this.Property(t => t.ProductName).HasColumnName("TenHangHoa");
           // this.Property(t => t.ManufacturerId).HasColumnName("NhaSanXuatId");
            this.Property(t => t.CategoryId).HasColumnName("DanhMucHangHoaID");
            this.Property(t => t.AdditionalShippingCharge).HasColumnName("PhiGiaoHangPhatSinh");
          //  this.Property(t => t.AdminComment).HasColumnName("GhiChuTuQuanLy");
            this.Property(t => t.DisplayOrder).HasColumnName("ThuTuHienThi");
            this.Property(t => t.FullDescription).HasColumnName("MoTaDayDu");
            this.Property(t => t.IsFreeShipping).HasColumnName("GiaoHangMienPhi");
            this.Property(t => t.IsShipEnabled).HasColumnName("CoGiaoHang");
               this.Property(t => t.MetaDescription).HasColumnName("MetaDescription");
               this.Property(t => t.MetaKeywords).HasColumnName("MetaKeywords");
             this.Property(t => t.MetaTitle).HasColumnName("MetaTitle");            
                this.Property(t => t.Price).HasColumnName("Gia");
          //  this.Property(t => t.Available).HasColumnName("CoHang");
          //  this.Property(t => t.ManufacturerPartNumber).HasColumnName("ManufacturerPartNumber");
             this.Property(t => t.Published).HasColumnName("HienThi");
             this.Property(t => t.ShortDescription).HasColumnName("MoTaNgan");
             this.Property(t => t.ShowOnHomePage).HasColumnName("HienThiTrangChu");
             this.Property(t => t.StockQuantity).HasColumnName("SoLuong");
           //  this.Property(t => t.ManufacturerId).HasColumnName("NhaSanXuatId");

             // Relationships  
            //this.HasOptional(t => t.Category)
            //    .WithMany(t => t.Products)
            //    .HasForeignKey(d => d.CategoryId);
            //this.HasOptional(t => t.Manufacturer)
            //    .WithMany(t => t.Products)
            //    .HasForeignKey(d => d.ManufacturerId);
            //Store procedures
            this.MapToStoredProcedures();
        }
    }
}