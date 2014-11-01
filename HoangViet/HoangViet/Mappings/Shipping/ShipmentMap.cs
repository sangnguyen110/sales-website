using HoangViet.Models.Shipping;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Shipping
{
    public class ShipmentMap : EntityTypeConfiguration<Shipment>
    {
        public ShipmentMap()
        {
            this.HasKey(t => t.Id);

            // Properties


            // Table & Column Mappings
            this.ToTable("GiaoHang");
            this.Property(t => t.Id).HasColumnName("GiaoHangId");
            this.Property(t => t.OrderId).HasColumnName("DonDatHangId");
            this.Property(t => t.ShipperId).HasColumnName("DichVuGiaoHangId");
            this.Property(t => t.TrackingNumber).HasColumnName("TrackingNumber");
            this.Property(t => t.ShippedDateUtc).HasColumnName("NgayGiaoHang");
            this.Property(t => t.DeliveryDateUtc).HasColumnName("NgayNhanHang");
            this.Property(t => t.ShippingExclTax).HasColumnName("PhiGiaoHangChuaVAT");
            this.Property(t => t.ShippingInclTax).HasColumnName("PhiGiaoHangCoVAT");


            this.MapToStoredProcedures();
        }
    }
}