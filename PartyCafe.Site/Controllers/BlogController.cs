using System.Web.Mvc;

namespace PartyCafe.Site.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
    }
}