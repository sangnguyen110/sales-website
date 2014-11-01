using HoangViet.Models.Shipping;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Shipping
{
    public class ShipmentDetailMap : EntityTypeConfiguration<ShipmentDetail>
    {
        public ShipmentDetailMap()
        {
            this.HasKey(t => t.Id);

            // Properties


            // Table & Column Mappings
            this.ToTable("ChiTietGiaoHang");
            this.Property(t => t.Id).HasColumnName("ChiTietGiaoHangId");
            this.Property(t => t.OrderDetailId).HasColumnName("ChiTietDatHangId");
            this.Property(t => t.ShipmentId).HasColumnName("GiaoHangId");
            this.Property(t => t.Quantity).HasColumnName("SoLuong");


            this.MapToStoredProcedures();
        }
    }
}