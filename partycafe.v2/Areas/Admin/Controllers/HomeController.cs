using System.Web.Mvc;

namespace partycafev2.Areas.Admin.Controllers
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