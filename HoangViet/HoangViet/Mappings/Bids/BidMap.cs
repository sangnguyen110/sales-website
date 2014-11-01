using HoangViet.Models.Bids;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Bids
{
    public class BidMap : EntityTypeConfiguration<Bid>
    {
        public BidMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties


            // Table & Column Mappings
            this.ToTable("DauGia");
            this.Property(t => t.Id).HasColumnName("DauGiaId");
            this.Property(t => t.Price).HasColumnName("TenPhienDauGia");
            this.Property(t => t.ListingID).HasColumnName("PhienDauGiaId");
            this.Property(t => t.TimeBid).HasColumnName("ThoiGianDatGia");

            this.MapToStoredProcedures();
        }
    }
}