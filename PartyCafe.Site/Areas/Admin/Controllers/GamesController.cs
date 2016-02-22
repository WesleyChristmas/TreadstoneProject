using System.Web.Mvc;
using BusinessEntity;
using BusinessInterface;
using PartyCafe.Site.Areas.Admin.Core;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class GamesController : Controller
    {
        // GET: Admin/Games
        public ActionResult Index()
        {
            return View();
        }
    }
}