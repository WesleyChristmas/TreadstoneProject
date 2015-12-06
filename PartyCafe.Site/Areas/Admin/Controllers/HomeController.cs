using System.Web.Mvc;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}