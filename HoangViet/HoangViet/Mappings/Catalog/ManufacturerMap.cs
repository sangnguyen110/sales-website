using HoangViet.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Catalog
{
    public class ManufacturerMap : EntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
         

            // Table & Column Mappings
            this.ToTable("NhaSanXuat");
            this.Property(t => t.Id).HasColumnName("NhaSanXuatId");
            this.Property(t => t.CreatedOnUtc).HasColumnName("NgayTao");
            this.Property(t => t.Deleted).HasColumnName("DaXoa");
            this.Property(t => t.Description).HasColumnName("Mota");
            this.Property(t => t.DisplayOrder).HasColumnName("ThuTuHienThi");
            this.Property(t => t.Name).HasColumnName("TenNhaSanXuat");
            this.Property(c => c.PictureId).HasColumnName("HinhAnh");
            this.Property(c => c.PriceRanges).HasColumnName("MucGia");
            this.Property(c => c.Published).HasColumnName("HienThi");
            this.Property(c => c.UpdatedOnUtc).HasColumnName("NgayCapNhat");

    
            //Store procedures
            this.MapToStoredProcedures();
        }
    }
}