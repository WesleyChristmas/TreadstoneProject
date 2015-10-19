using System.Web.Mvc;

namespace PartyCafe.Site.Controllers
{
    public class FoodMenuController : Controller
    {
        // GET: FoodMenu
        public ActionResult Index()
        {
            return View();
        }
    }
}