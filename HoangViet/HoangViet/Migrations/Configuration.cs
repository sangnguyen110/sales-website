namespace HoangViet.Migrations
{
    using HoangViet.Models;
    using HoangViet.Models.Accounts;
    using HoangViet.Models.Catalog;
    using HoangViet.Models.Common;
    using HoangViet.Models.Orders;
    using HoangViet.Models.Shipping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HoangViet.Models.HoangVietDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(HoangViet.Models.HoangVietDbContext context)
        {
            CatalogSeed(context);
            ShippingSeed(context);
            AddressesSeed(context);
            OrdersSeed(context);
            ShipmentsSeed(context);

        }
        private void CatalogSeed(HoangVietDbContext context)
        {    //  seed categories
            context.Categories.AddOrUpdate(c => c.CategoryName,
                new Category { CategoryName = "Wireless Network", Published = true },
                new Category { CategoryName = "Wired Network", Published = true });
            // seed products
            context.Products.AddOrUpdate(p => p.ProductName,
                new Product
                {
                    ProductName = "WAPS-APG600H",
                    ShortDescription = @"Compliant with IEEE802.3af Power over Ethernet, the WAPS-APG600H can be powered with 802.3af PoE unit.",
                    Published = true,
                    Category = context.Categories.FirstOrDefault(c => c.CategoryName == "Wireless Network"),
                    Price = 1000000
                },
                new Product
                {
                    ProductName = "BS-2116U",
                    ShortDescription = @"Designed to IEEE802.11n Draft 2.0 Specifications Wireless Connections up to 300Mbps*",
                    Published = true,
                    Category = context.Categories.FirstOrDefault(c => c.CategoryName == "Wireless Network"),
                    Price = 2000000
                });
        }

        private void ShippingSeed(HoangVietDbContext context)
        {  // seed shippingmethods
            context.ShippingMethods.AddOrUpdate(sm => sm.ShippingMethodName,
                new ShippingMethod { ShippingMethodName = "DHL", Sex = Gender.Female, Phone = "01227970951", ContactName = "Hằng" },
                new ShippingMethod { ShippingMethodName = "Viettel", Sex = Gender.Female, Phone = "01227970952", ContactName = "Hồng" }
                );
        }

        private void AddressesSeed(HoangVietDbContext context)
        {
            // seed citys
            context.Citys.AddOrUpdate(c => c.CityName,
                new City { CityName = "Hồ Chí Minh" }, new City { CityName = "Đà Nẵng" });

            // seed districts
            context.Districts.AddOrUpdate(d => d.DistrictName,
                new District { DistrictName = "Quận Thủ Đức", CityId = context.Citys.FirstOrDefault(c => c.CityName == "Hồ Chí Minh").Id },
                new District { DistrictName = "Quận 9", CityId = context.Citys.FirstOrDefault(c => c.CityName == "Hồ Chí Minh").Id },
                new District { DistrictName = "Quận Tân Bình", CityId = context.Citys.FirstOrDefault(c => c.CityName == "Hồ Chí Minh").Id },
                new District { DistrictName = "Quận 1", CityId = context.Citys.FirstOrDefault(c => c.CityName == "Hồ Chí Minh").Id },
                new District { DistrictName = "Quận Thanh Khê", CityId = context.Citys.FirstOrDefault(c => c.CityName == "Đà Nẵng").Id },
                new District { DistrictName = "Quận Sơn Trà", CityId = context.Citys.FirstOrDefault(c => c.CityName == "Đà Nẵng").Id },
                new District { DistrictName = "Quận Ngũ Hành Sơn", CityId = context.Citys.FirstOrDefault(c => c.CityName == "Đà Nẵng").Id },
                new District { DistrictName = "Quận Cẩm Lệ", CityId = context.Citys.FirstOrDefault(c => c.CityName == "Đà Nẵng").Id });

            // seed addresses
            context.Addresses.AddOrUpdate(a => a.Email,
        new Address
        {
            DistrictId = context.Districts.FirstOrDefault(d => d.DistrictName == "Quận Thủ Đức").Id,
            CityId = context.Citys.FirstOrDefault(c => c.CityName == "Hồ Chí Minh").Id,
            CustomerId = context.Users.FirstOrDefault(c => c.Email == "User1@yahoo.com").Id,
            FullName = "User 5",
            Address1 = "Số 50 đường 19",
            Company = "company 5",
            DefaultBillingAddress = true,
            DefaultShippingAddress = true,
            Email = "User5@yahoo.com",
            FaxNumber = "37222876",
            PhoneNumber = "01227970375"
        },
        new Address
        {
            DistrictId = context.Districts.FirstOrDefault(d => d.DistrictName == "Quận Thủ Đức").Id,
            CityId = context.Citys.FirstOrDefault(c => c.CityName == "Hồ Chí Minh").Id,
            CustomerId = context.Users.FirstOrDefault(c => c.Email == "User1@yahoo.com").Id,
            FullName = "User 6",
            Address1 = "Số 60 đường 20",
            Company = "company 6",
            DefaultBillingAddress = false,
            DefaultShippingAddress = false,
            Email = "User6@yahoo.com",
            FaxNumber = "37222876",
            PhoneNumber = "01227970376"
        },
        new Address
        {
            DistrictId = context.Districts.FirstOrDefault(d => d.DistrictName == "Quận Thanh Khê").Id,
            CityId = context.Citys.FirstOrDefault(c => c.CityName == "Đà Nẵng").Id,
            CustomerId = context.Users.FirstOrDefault(c => c.Email == "User1@yahoo.com").Id,
            FullName = "User 7",
            Address1 = "Số 70 đường 21",
            Company = "company 7",
            DefaultBillingAddress = false,
            DefaultShippingAddress = false,
            Email = "User7@yahoo.com",
            FaxNumber = "37222877",
            PhoneNumber = "01227970377"
        },
        new Address
        {
            DistrictId = context.Districts.FirstOrDefault(d => d.DistrictName == "Quận Sơn Trà").Id,
            CityId = context.Citys.FirstOrDefault(c => c.CityName == "Đà Nẵng").Id,
            CustomerId = context.Users.FirstOrDefault(c => c.Email == "User2@yahoo.com").Id,
            FullName = "User 8",
            Address1 = "Số 80 đường 22",
            Company = "company 8",
            DefaultBillingAddress = true,
            DefaultShippingAddress = false,
            Email = "User8@yahoo.com",
            FaxNumber = "37222878",
            PhoneNumber = "01227970378"
        },
        new Address
        {
            DistrictId = context.Districts.FirstOrDefault(d => d.DistrictName == "Quận 1").Id,
            CityId = context.Citys.FirstOrDefault(c => c.CityName == "Hồ Chí Minh").Id,
            CustomerId = context.Users.FirstOrDefault(c => c.Email == "User2@yahoo.com").Id,
            FullName = "User 9",
            Address1 = "Số 90 đường 23",
            Company = "company 9",
            DefaultBillingAddress = false,
            DefaultShippingAddress = true,
            Email = "User9@yahoo.com",
            FaxNumber = "37222879",
            PhoneNumber = "01227970379"
        });

        }

        private void OrdersSeed(HoangVietDbContext context)
        {
            context.Orders.AddOrUpdate(
                new Order
                {
                    CustomerId = context.Users.FirstOrDefault(c => c.Email == "User1@yahoo.com").Id,
                    BillingAddressId = context.Addresses.FirstOrDefault(a => a.Customer.Email == "User2@yahoo.com").Id,
                    OrderDate = DateTime.Now,
                    OrderStatus = OrderStatus.Pending,
                    OrderTotal = 1000000,
                    PaymentStatus = Models.Payments.PaymentStatus.Pending,
                    ShippingAddressId = context.Addresses.FirstOrDefault(a => a.Customer.Email == "User1@yahoo.com").Id,
                    ShippingStatus = ShippingStatus.NotYetShipped,
                    PaymentMethod = Models.Payments.PaymentMethod.CashAtStore,
                    TaxRates = 10
                },
                 new Order
                 {
                     CustomerId = context.Users.FirstOrDefault(c => c.Email == "User1@yahoo.com").Id,
                     BillingAddressId = context.Addresses.FirstOrDefault(a => a.Customer.Email == "User2@yahoo.com").Id,
                     OrderDate = DateTime.Now,
                     OrderStatus = OrderStatus.Pending,
                     OrderTotal = 1200000,
                     PaymentStatus = Models.Payments.PaymentStatus.Pending,
                     ShippingAddressId = context.Addresses.FirstOrDefault(a => a.Customer.Email == "User2@yahoo.com").Id,
                     ShippingStatus = ShippingStatus.NotYetShipped,
                     PaymentMethod = Models.Payments.PaymentMethod.CashAtStore,
                     TaxRates = 10
                 },
                 new Order
                 {
                     CustomerId = context.Users.FirstOrDefault(c => c.Email == "User2@yahoo.com").Id,
                     BillingAddressId = context.Addresses.FirstOrDefault(a => a.Customer.Email == "User2@yahoo.com").Id,
                     OrderDate = DateTime.Now,
                     OrderStatus = OrderStatus.Pending,
                     OrderTotal = 1200000,
                     PaymentStatus = Models.Payments.PaymentStatus.Pending,
                     ShippingStatus = ShippingStatus.ShippingNotRequired,
                     PaymentMethod = Models.Payments.PaymentMethod.CashAtStore,
                     TaxRates = 10
                 },
                 new Order
                 {
                     CustomerId = context.Users.FirstOrDefault(c => c.Email == "User2@yahoo.com").Id,
                     BillingAddressId = context.Addresses.FirstOrDefault(a => a.Customer.Email == "User2@yahoo.com").Id,
                     OrderDate = DateTime.Now,
                     OrderStatus = OrderStatus.Complete,
                     OrderTotal = 1200000,
                     PaymentStatus = Models.Payments.PaymentStatus.Paid,
                     ShippingAddressId = context.Addresses.FirstOrDefault(a => a.Customer.Email == "User2@yahoo.com").Id,
                     ShippingStatus = ShippingStatus.Delivered,
                     PaymentMethod = Models.Payments.PaymentMethod.CashAtStore,
                     TaxRates = 10
                 });
            context.OrderDetails.AddOrUpdate(c => new { c.OrderId, c.ProductId },
                new OrderDetail
                {
                    OrderId = context.Orders.FirstOrDefault(o => o.Customer.Email == "User1@yahoo.com").Id,
                    ProductId = context.Products.FirstOrDefault(p => p.ProductName == "WAPS-APG600H").Id,
                    Quantity = 10,
                    UnitPriceExclTax = 100000,
                    PriceExclTax = 10*100000
                }
                ,
                new OrderDetail
                {
                    OrderId = context.Orders.FirstOrDefault(o => o.Customer.Email == "User1@yahoo.com").Id,
                    ProductId = context.Products.FirstOrDefault(p => p.ProductName == "BS-2116U").Id,
                    Quantity = 10,
                    UnitPriceExclTax = 100000,
                    PriceExclTax = 10 * 100000
                },
                 new OrderDetail
                 {
                     OrderId = context.Orders.FirstOrDefault(o => o.Customer.Email == "User2@yahoo.com" && o.ShippingStatus == ShippingStatus.Delivered).Id,
                     ProductId = context.Products.FirstOrDefault(p => p.ProductName == "WAPS-APG600H").Id,
                     Quantity = 10,
                     UnitPriceExclTax = 100000,
                     PriceExclTax = 10 * 100000
                 },
                new OrderDetail
                {
                    OrderId = context.Orders.FirstOrDefault(o => o.Customer.Email == "User2@yahoo.com" && o.ShippingStatus == ShippingStatus.Delivered).Id,
                    ProductId = context.Products.FirstOrDefault(p => p.ProductName == "BS-2116U").Id,
                    Quantity = 10,
                    UnitPriceExclTax = 100000,
                    PriceExclTax = 10 * 100000
                }
                );
        }

        private void ShipmentsSeed(HoangVietDbContext context)
        {
            context.Shipments.AddOrUpdate(sm => sm.TrackingNumber,
                new Shipment
                {
                    OrderId = context.Orders.FirstOrDefault(o => 
                        o.Customer.Email == "User2@yahoo.com" && o.ShippingStatus == ShippingStatus.Delivered ).Id,
                    ShipperId = context.ShippingMethods.FirstOrDefault(sm => sm.ShippingMethodName == "DHL").Id,
                    TotalWeight = 100,
                    TrackingNumber = "12345",
                    ShippedDateUtc = DateTime.Now.Date,
                    DeliveryDateUtc = DateTime.Now.Date.AddDays(5),
                    ShippingExclTax = 150000,
                    ShippingInclTax = 165000
                },
                new Shipment {
                    OrderId = context.Orders.FirstOrDefault(o => 
                        o.Customer.Email == "User2@yahoo.com" && o.ShippingStatus == ShippingStatus.Delivered).Id,
                    ShipperId = context.ShippingMethods.FirstOrDefault(sm => sm.ShippingMethodName == "DHL").Id,
                    TotalWeight = 50,
                    TrackingNumber = "12346",
                    ShippedDateUtc = DateTime.Now.Date.AddDays(7),
                    DeliveryDateUtc = DateTime.Now.Date.AddDays(10),
                    ShippingExclTax = 200000,
                    ShippingInclTax = 220000
                });
            context.ShipmentDetails.AddOrUpdate(sd => new { sd.ShipmentId, sd.OrderDetailId },
                new ShipmentDetail
                {
                    ShipmentId = context.Shipments.FirstOrDefault(sm => sm.TrackingNumber == "12345").Id,
                    OrderDetailId = context.OrderDetails.FirstOrDefault(od =>
                        od.Order.Customer.Email == "User2@yahoo.com"
                        && od.Order.ShippingStatus == ShippingStatus.Delivered
                        && od.Product.ProductName == "WAPS-APG600H").Id,
                    Quantity = 2
                }, new ShipmentDetail
                {
                    ShipmentId = context.Shipments.FirstOrDefault(sm => sm.TrackingNumber == "12345").Id,
                    OrderDetailId = context.OrderDetails.FirstOrDefault(od =>
                        od.Order.Customer.Email == "User2@yahoo.com"
                        && od.Order.ShippingStatus == ShippingStatus.Delivered
                        && od.Product.ProductName == "BS-2116U").Id,
                    Quantity = 10
                }, new ShipmentDetail
                {
                    ShipmentId = context.Shipments.FirstOrDefault(sm => sm.TrackingNumber == "12346").Id,
                    OrderDetailId = context.OrderDetails.FirstOrDefault(od =>
                        od.Order.Customer.Email == "User2@yahoo.com"
                        && od.Order.ShippingStatus == ShippingStatus.Delivered
                        && od.Product.ProductName == "WAPS-APG600H").Id,
                    Quantity = 8
                }
                );
        }
    }
}
