using HoangViet.Models.Bids;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Bids
{
    public class ListingMap : EntityTypeConfiguration<Listing>
    {
        public ListingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
      

            // Table & Column Mappings
            this.ToTable("PhienDauGia");
            this.Property(t => t.Id).HasColumnName("PhienDauGiaId");
            this.Property(t => t.Title).HasColumnName("TenPhienDauGia");
            this.Property(t => t.CountBid).HasColumnName("SoLuongDauGia");
            this.Property(t => t.Description).HasColumnName("MoTaPhienDauGia");
            this.Property(t => t.DisplayOrder).HasColumnName("ThuTuHienThi");
            this.Property(t => t.EndTime).HasColumnName("ThoiGianKetThuc");
            this.Property(c => c.HighestPrice).HasColumnName("GiaCaoNhatHienTai");
            this.Property(c => c.Increment).HasColumnName("BuocGia");
            this.Property(c => c.ProductId).HasColumnName("SanPhamId");
            this.Property(c => c.Quantity).HasColumnName("SoLuong");
            this.Property(c => c.StartingPrice).HasColumnName("GiaKhoiDiem");
            this.Property(c => c.StartTime).HasColumnName("ThoiGianBatDau");
            this.Property(c => c.WinningPrice).HasColumnName("GiaThang");

            this.MapToStoredProcedures();
        }
    }
}