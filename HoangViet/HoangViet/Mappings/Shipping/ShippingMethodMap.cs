using HoangViet.Models.Shipping;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Shipping
{
    public class ShippingMethodMap:EntityTypeConfiguration<ShippingMethod>
    {
        public ShippingMethodMap()
        {
           

            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ShippingMethodName)
                .IsRequired()
                .HasMaxLength(200);
            this.Property(c => c.Description).HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("PhuongThucGiaoHang");
            this.Property(t => t.Id).HasColumnName("PhuongThucGiaoHangId");
            this.Property(t => t.ShippingMethodName).HasColumnName("TenPhuongThucGiaoHang");
            this.Property(t => t.Description).HasColumnName("MoTa");
            this.Property(t => t.Phone).HasColumnName("SoLienLac");
            this.Property(t => t.ContactName).HasColumnName("TenLienHe");
            this.Property(t => t.Sex).HasColumnName("GioiTinh");  


            this.MapToStoredProcedures();
        }
    }
}