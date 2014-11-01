using HoangViet.Models.Orders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Orders
{
    public partial class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
   
     

            // Table & Column Mappings
            this.ToTable("DonDatHang");
            this.Property(t => t.Id).HasColumnName("DonDatHangId");
            this.Property(t => t.CustomerId).HasColumnName("KhachHangId");
            this.Property(t => t.OrderDate).HasColumnName("NgayDatHang");
            this.Property(t => t.BillingAddressId).HasColumnName("DiaChiHoaDonId");
            this.Property(t => t.ShippingAddressId).HasColumnName("DiaChiGiaoHangId");
            this.Property(t => t.OrderStatus).HasColumnName("TrangThaiDatHang");
            this.Property(c => c.ShippingStatus).HasColumnName("TrangThaiGiaoHang");
            this.Property(c => c.PaymentStatus).HasColumnName("TrangThaiThanhToan");
            this.Property(c => c.PaymentMethod).HasColumnName("PhuongThucThanhToan");
           // this.Property(c => c.VatNumber).HasColumnName("MaSoThue");
            this.Property(c => c.OrderSubtotalInclTax).HasColumnName("TongDonHangCoThue");
            this.Property(c => c.OrderSubtotalExclTax).HasColumnName("TongDonHangChuaThue");
            this.Property(c => c.OrderShippingInclTax).HasColumnName("TongGiaoHangCoThue");
            this.Property(c => c.OrderShippingExclTax).HasColumnName("TongGiaoHangChuaThue");
            this.Property(c => c.TaxRates).HasColumnName("ThueSuat");
            this.Property(c => c.OrderTax).HasColumnName("Thue");
            this.Property(c => c.OrderTotal).HasColumnName("TongDonHang");
            this.Property(c => c.PaidDateUtc).HasColumnName("NgayThanhToan");



            //Store procedures
            this.MapToStoredProcedures();
           


        }
    }
}