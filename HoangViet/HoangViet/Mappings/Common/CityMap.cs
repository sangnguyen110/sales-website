using HoangViet.Models.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace HoangViet.Mappings.Common
{
    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            
            // Primary Key
            this.HasKey(a => a.Id);

            // Properties
          

            // Table & Column Mappings
            this.ToTable("ThanhPho");
            this.Property(t => t.Id).HasColumnName("ThanhPhoId");
            this.Property(t => t.CityName).HasColumnName("TenThanhPho");
            this.Property(t => t.Published).HasColumnName("HienThi");
            this.Property(t => t.DisplayOrder).HasColumnName("ThuTuHienThi");
      

            this.MapToStoredProcedures();
           


        }
    }
}