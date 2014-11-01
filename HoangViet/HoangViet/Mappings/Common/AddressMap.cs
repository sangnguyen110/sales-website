using HoangViet.Models.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Common
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            
            // Primary Key
            this.HasKey(a => a.Id);

            // Properties
       

            // Table & Column Mappings
            this.ToTable("DiaChi");
            this.Property(t => t.Id).HasColumnName("DiaChiId");
            this.Property(t => t.FullName).HasColumnName("TenHo");
            this.Property(t => t.Email).HasColumnName("DiaChiEmail");
            this.Property(t => t.Company).HasColumnName("TenCongTy");
            this.Property(t => t.CityId).HasColumnName("ThanhPhoId");
            this.Property(t => t.DistrictId).HasColumnName("QuanId");
            this.Property(c => c.Address1).HasColumnName("SoNha");
            this.Property(c => c.PhoneNumber).HasColumnName("SoDienThoai");
            this.Property(c => c.FaxNumber).HasColumnName("SoFax");
            this.Property(c => c.CustomerId).HasColumnName("KhachHangId");

            this.MapToStoredProcedures();
           

            
        }
    }
}