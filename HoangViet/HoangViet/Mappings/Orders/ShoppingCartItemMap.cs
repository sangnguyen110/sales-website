using HoangViet.Models.Orders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Orders
{
    public partial class ShoppingCartItemMap : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties



            // Table & Column Mappings
            this.ToTable("ChiTietGioHang");
            this.Property(t => t.Id).HasColumnName("ChiTietGioHangId");
            this.Property(t => t.CustomerId).HasColumnName("KhachHangId");
            this.Property(t => t.CreatedOnUtc).HasColumnName("NgayTao");
            this.Property(t => t.ProductId).HasColumnName("HangHoaId");
            this.Property(t => t.Quantity).HasColumnName("SoLuong");
            this.Property(t => t.ShoppingCartType).HasColumnName("LoaiGioHang");
           
            // this.Property(c => c.VatNumber).HasColumnName("MaSoThue");
  



            //Store procedures
            this.MapToStoredProcedures();
        }
    }
}