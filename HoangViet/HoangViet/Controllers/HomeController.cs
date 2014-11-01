using HoangViet.ViewModels;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HoangViet.Controllers {
    public class HomeController : BaseController {
        public async Task<ActionResult> Index()
        {

            var homepageViewModel = new HomepageViewModel { 
            FeatureProducts = await Db.Products.Include(p => p.Category).Where(p => p.Published && p.IsFeaturedProduct)
                                .OrderByDescending(p => p.DisplayOrder).ToListAsync(),
            LatestProducts = await Db.Products.Include(p => p.Category).Where(p => p.Published)
                                .OrderByDescending(p => p.Id).ToListAsync()
            };
            homepageViewModel.CountFeatureProducts = homepageViewModel.FeatureProducts.Count;
            return View(homepageViewModel);
        }

        [Authorize]
        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

     
    }
}
