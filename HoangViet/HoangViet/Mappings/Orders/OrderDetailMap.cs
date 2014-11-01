using HoangViet.Models.Orders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Orders
{
    public partial class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
     

            // Table & Column Mappings
            this.ToTable("ChiTietDonDatHang");
            this.Property(t => t.Id).HasColumnName("ChiTietDonId");
            this.Property(t => t.OrderId).HasColumnName("DonDatHangId");
            this.Property(t => t.ProductId).HasColumnName("SanPhamId");
            this.Property(t => t.Quantity).HasColumnName("SoLuong");
            this.Property(t => t.PriceExclTax).HasColumnName("TongGiaChuaThue");
            this.Property(t => t.PriceInclTax).HasColumnName("TongGiaCoThue");
            this.Property(c => c.UnitPriceExclTax).HasColumnName("GiaChuaThue");
            this.Property(c => c.UnitPriceInclTax).HasColumnName("GiaCoThue");
           


            //Store procedures
            this.MapToStoredProcedures();
           


        }
    }
}