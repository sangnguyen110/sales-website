using HoangViet.Mappings.Bids;
using HoangViet.Mappings.Catalog;
using HoangViet.Mappings.Common;
using HoangViet.Mappings.Orders;
using HoangViet.Mappings.Shipping;
using HoangViet.Models.Accounts;
using HoangViet.Models.Bids;
using HoangViet.Models.Catalog;
using HoangViet.Models.Common;
using HoangViet.Models.Orders;
using HoangViet.Models.Shipping;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HoangViet.Models
{
    public class HoangVietDbContext : IdentityDbContext<HoangVietUser>
    {

        public HoangVietDbContext()
            : base("HoangVietConnection", throwIfV1Schema: false)
        {

            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;


        }

        static HoangVietDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            //   Database.SetInitializer<HoangVietDbContext>(new HoangVietDbInitializer());
        }

        public static HoangVietDbContext Create()
        {
            return new HoangVietDbContext();
            //var hoangVietDbContext = (HoangVietDbContext) DependencyResolver.Current.GetService<IDataContextAsync>();
            //return hoangVietDbContext;
        }
        public System.Data.Entity.DbSet<HoangViet.Models.Catalog.Category> Categories { get; set; }

        //public System.Data.Entity.DbSet<HoangViet.Models.Catalog.Picture> Pictures { get; set; }
        public System.Data.Entity.DbSet<HoangViet.Models.Catalog.Product> Products { get; set; }
        public System.Data.Entity.DbSet<HoangViet.Models.Shipping.ShippingMethod> ShippingMethods { get; set; }
        public System.Data.Entity.DbSet<HoangViet.Models.Common.District> Districts { get; set; }
        public System.Data.Entity.DbSet<HoangViet.Models.Common.City> Citys { get; set; }
        public System.Data.Entity.DbSet<HoangViet.Models.Common.Address> Addresses { get; set; }
        public System.Data.Entity.DbSet<HoangViet.Models.Orders.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<HoangViet.Models.Shipping.Shipment> Shipments { get; set; }
        public System.Data.Entity.DbSet<HoangViet.Models.Orders.OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<HoangViet.Models.Shipping.ShipmentDetail> ShipmentDetails { get; set; }

        public System.Data.Entity.DbSet<HoangViet.Models.Orders.ShoppingCartItem> ShoppingCartItems { get; set; }

        public System.Data.Entity.DbSet<Manufacturer> Manufacturers { get; set; }
        public System.Data.Entity.DbSet<Listing> Listings { get; set; }
        public System.Data.Entity.DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //        modelBuilder.Entity<IdentityUserLogin>().HasKey<int>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ShippingMethodMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ShipmentMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new ShipmentDetailMap());
            modelBuilder.Configurations.Add(new ShoppingCartItemMap());
            modelBuilder.Configurations.Add(new ListingMap());
            modelBuilder.Configurations.Add(new BidMap());
            modelBuilder.Configurations.Add(new ManufacturerMap());

            //modelBuilder.Configurations.Add(new PictureMap());
            //  modelBuilder.Entity<Picture>().HasMany(t => t.Products).WithMany(t => t.Pictures).Map(t => t.ToTable("HangHoaHinhAnh").MapLeftKey("HangHoaId").MapRightKey("HinhAnhId"));
            // modelBuilder.Entity<Picture>().HasMany(t => t.Categories).WithOptional(t => t.Picture).HasForeignKey(t => t.PictureId);
            modelBuilder.Entity<Product>().HasOptional(t => t.Category)
                .WithMany(t => t.Products).HasForeignKey(d => d.CategoryId);
            modelBuilder.Entity<Category>().HasOptional(t => t.ParentCategory)
                .WithMany(t => t.ChildCategories).HasForeignKey(d => d.ParentCategoryId).WillCascadeOnDelete(false);
            //   modelBuilder.Entity<Category>().HasOptional(t => t.Picture).WithMany(t => t.Categories).HasForeignKey(d => d.PictureId);
            modelBuilder.Entity<District>().HasRequired(a => a.City)
                .WithMany(c => c.Districts)
                .HasForeignKey(a => a.CityId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>().HasOptional(a => a.District)
                .WithMany(d => d.Addresses)
                .HasForeignKey(a => a.DistrictId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Address>().HasOptional(a => a.City)
                .WithMany(d => d.Addresses)
                .HasForeignKey(a => a.CityId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Order>().HasRequired(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>().HasOptional(o => o.BillingAddress)
                .WithMany()
                .HasForeignKey(o => o.BillingAddressId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Order>().HasOptional(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Address>().HasRequired(a => a.Customer)
                .WithMany(c => c.Addresses).HasForeignKey(a => a.CustomerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasOptional(a => a.Order)
                .WithMany(o => o.Shipments).HasForeignKey(a => a.OrderId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasOptional(s => s.Shipper)
                .WithMany(sp => sp.Shipments).HasForeignKey(s => s.ShipperId).WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderDetail>().HasRequired(s => s.Order)
                .WithMany(o => o.OrderDetails).HasForeignKey(o => o.OrderId).WillCascadeOnDelete(true);
            modelBuilder.Entity<ShipmentDetail>().HasRequired(s => s.Shipment)
                .WithMany(o => o.ShipmentDetails).HasForeignKey(o => o.ShipmentId).WillCascadeOnDelete(true);
            modelBuilder.Entity<ShipmentDetail>().HasRequired(s => s.OrderDetail)
                .WithMany(od => od.ShipmentDetails).HasForeignKey(o => o.OrderDetailId).WillCascadeOnDelete(true);
            modelBuilder.Entity<ShoppingCartItem>().HasRequired(s => s.Product)
            .WithMany().HasForeignKey(s=>s.ProductId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Product>().HasOptional(p => p.Manufacturer)
            .WithMany(m => m.Products).HasForeignKey(p => p.ManufacturerId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Listing>().HasRequired(l => l.Product)
                .WithMany().HasForeignKey(l => l.ProductId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Bid>().HasRequired(b => b.Listing)
            .WithMany(b => b.Bids).HasForeignKey(s => s.ListingID).WillCascadeOnDelete(true);



            base.OnModelCreating(modelBuilder);
        }



    }
}