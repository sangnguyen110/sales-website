using HoangViet.Models.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Common
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            
            // Primary Key
            this.HasKey(a => a.Id);

            // Properties
          

            // Table & Column Mappings
            this.ToTable("QuanHuyen");
            this.Property(t => t.Id).HasColumnName("QuanHuyenId");
            this.Property(t => t.DistrictName).HasColumnName("TenQuan");
            this.Property(t => t.Published).HasColumnName("HienThi");
            this.Property(t => t.DisplayOrder).HasColumnName("ThuTuHienThi");
         

            this.MapToStoredProcedures();
           


        }
    }
}