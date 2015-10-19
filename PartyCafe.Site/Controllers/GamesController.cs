using System.Web.Mvc;

namespace PartyCafe.Site.Controllers
{
    public class GamesController : Controller
    {
        // GET: Games
        public ActionResult Index()
        {
            return View();
        }
    }
}