using HoangViet.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Globalization;

namespace HoangViet.Models.Orders
{
    public partial class ShoppingCart
    {
        private HoangVietDbContext _db;

        public ShoppingCart(HttpContextBase context, HoangVietDbContext db)
        {
            _db = db;
            this.ShoppingCartId = this.GetCartId(context);
        }
        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context, HoangVietDbContext db)
        {
            var cart = new ShoppingCart(context, db);
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller, HoangVietDbContext db)
        {
            return GetCart(controller.HttpContext, db);
        }

        public async Task AddToCartAsync(Product product)
        {
            // Get the matching cart and album instances
            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
            c => c.CustomerId == ShoppingCartId && c.ProductId == product.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new ShoppingCartItem
                {
                    ProductId = product.Id,
                    CustomerId = ShoppingCartId,
                    Quantity = 1,
                    CreatedOnUtc = DateTime.Now
                };

                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Quantity++;
            }

            // Save changes
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(List<ShoppingCartItem> UpdatedCartItems)
        {
            foreach(var updatedCartItem in UpdatedCartItems){
                var dbCartItem = _db.ShoppingCartItems
                    .SingleOrDefault(c => c.CustomerId == ShoppingCartId && c.Id == updatedCartItem.Id);
                if (updatedCartItem.Deleted || updatedCartItem.Quantity == 0)
                    _db.ShoppingCartItems.Remove(dbCartItem);
                else
                   dbCartItem.Quantity = updatedCartItem.Quantity;
            }

            await _db.SaveChangesAsync();           
        }
     

        public async Task EmptyCartAsync()
        {
            var cartItems = _db.ShoppingCartItems.Where(cart => cart.CustomerId == ShoppingCartId);
            _db.ShoppingCartItems.RemoveRange(cartItems);
            // Save changes
           // await _db.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartItem>> GetCartItemsAsync()
        {
            return await _db.ShoppingCartItems.Include(c => c.Product).Where(cart => cart.CustomerId == ShoppingCartId).ToListAsync();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _db.ShoppingCartItems
                          where cartItems.CustomerId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();

            // Return 0 if all entries are null
            return count ?? 0;
        }

        public string GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in _db.ShoppingCartItems
                              where cartItems.CustomerId == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Product.Price).Sum();
            var vietnameseTotal = "";
            if (total.HasValue)
                vietnameseTotal = GetVietnamesePirce(total.Value);
            else
                vietnameseTotal = GetVietnamesePirce(0);
            return vietnameseTotal;
        }
        public string GetVietnamesePirce(decimal amount)
        {
            System.Globalization.CultureInfo vietnam = new CultureInfo(1066);
            CultureInfo usa = new CultureInfo("en-US");

            NumberFormatInfo nfi = usa.NumberFormat;
            nfi = (NumberFormatInfo)nfi.Clone();
            NumberFormatInfo vnfi = vietnam.NumberFormat;
            nfi.CurrencySymbol = vnfi.CurrencySymbol;
            nfi.CurrencyNegativePattern = vnfi.CurrencyNegativePattern;
            nfi.CurrencyPositivePattern = vnfi.CurrencyPositivePattern;
            return amount.ToString("c0", nfi);

        }
        public string GetTotalTax()
        {           
            decimal? total = (from cartItems in _db.ShoppingCartItems
                              where cartItems.CustomerId == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Product.Price).Sum();
            var vietnameseTotal = "";
            if (total.HasValue)
                vietnameseTotal = GetVietnamesePirce(total.Value/10);
            else
                vietnameseTotal = GetVietnamesePirce(0);
            return vietnameseTotal;
        }

        public decimal GetTotalTaxValue()
        {
            decimal? total = (from cartItems in _db.ShoppingCartItems
                              where cartItems.CustomerId == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Product.Price).Sum();
            if (total.HasValue)
                total = total.Value / 10M;
            else
                total = 0M;
            return total.Value;
        }

        public string GetTotalInclTax()
        {
            decimal? total = (from cartItems in _db.ShoppingCartItems
                              where cartItems.CustomerId == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Product.Price).Sum();
            var vietnameseTotal = "";
            if (total.HasValue)
                vietnameseTotal = GetVietnamesePirce(total.Value*1.1M);
            else
                vietnameseTotal = GetVietnamesePirce(0);
            return vietnameseTotal;
        }

        public decimal GetTotalInclTaxValue()
        {
            decimal? total = (from cartItems in _db.ShoppingCartItems
                              where cartItems.CustomerId == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Product.Price).Sum();
            if (total.HasValue)
                total = total.Value * 1.1M;
            else
                total = 0M;
            return total.Value;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = await GetCartItemsAsync();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Order = order,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPriceExclTax = cartItem.Product.Price,
                    //PriceExclTax = Decimal.Parse(cartItem.Total)
                };
                orderDetail.CalOrderDetailAmounts();

                // Set the order total of the shopping cart
                orderTotal += (cartItem.Quantity * cartItem.Product.Price);

                _db.OrderDetails.Add(orderDetail);

            }

            // Set the order's total to the orderTotal count
            order.CalOrderAmount();

            // Save the order
        //    await _db.SaveChangesAsync();

            // Empty the shopping cart
            await EmptyCartAsync();

            // Return the OrderId as the confirmation number
            return order;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public async Task MigrateCartAsync(string customerId)
        {
            var shoppingCart = _db.ShoppingCartItems.Where(c => c.CustomerId == ShoppingCartId);

            foreach (ShoppingCartItem item in shoppingCart)
            {
                item.CustomerId = customerId;
            }
            await _db.SaveChangesAsync();
        }
    }
}