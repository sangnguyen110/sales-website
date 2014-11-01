using HoangViet.Models.Orders;
using HoangViet.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HoangViet.Controllers
{
    public class ShoppingCartController : BaseController
    {
       
        //
        // GET: /ShoppingCart/

        public async Task<ActionResult> Index()
        {
            var cart = new ShoppingCart(this.HttpContext,Db);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = await cart.GetCartItemsAsync(),
                CartTotal = cart.GetTotal(),
                CartTax = cart.GetTotalTax(),
                CartTotalIncTax = cart.GetTotalInclTax(),
                CartCount = cart.GetCount()
            };
            // Return the view
            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5

        public async Task<ActionResult> AddToCart(int id)
        {

            // Retrieve the album from the database
            var addedProduct = Db.Products.Single(p => p.Id == id);

            // Add it to the shopping cart
            var cart = new ShoppingCart(this.HttpContext, Db);

            await cart.AddToCartAsync(addedProduct);

            // Go back to the main store page for more shopping
            return Json(new {Total = cart.GetTotal(), CartCount = cart.GetCount()}, JsonRequestBehavior.AllowGet); ;
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5

        [HttpPost]
        public async Task<ActionResult> UpdateCart(ShoppingCartViewModel viewModel)
        {
            // Remove the item from the cart
            var cart = new ShoppingCart(this.HttpContext,Db);

            // Get the name of the album to display confirmation
            await cart.UpdateCartAsync(viewModel.CartItems);

            var newViewModel = new ShoppingCartViewModel
            {
                CartItems = await cart.GetCartItemsAsync(),
                CartTotal = cart.GetTotal(),
                CartTax = cart.GetTotalTax(),
                CartTotalIncTax = cart.GetTotalInclTax(),
                CartCount = cart.GetCount()
            };

            return PartialView("UpdateShoppingCartPartial", newViewModel);
        }

        ////
        //// GET: /ShoppingCart/CartSummary

        //[ChildActionOnly]
        //public ActionResult CartSummary()
        //{
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    ViewData["CartCount"] = cart.GetCount();

        //    return PartialView("CartSummary");
        //}
    }
}