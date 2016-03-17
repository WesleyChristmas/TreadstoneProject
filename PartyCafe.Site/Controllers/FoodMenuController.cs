using System.Web.Mvc;
using System.Collections.Generic;
using BusinessEntity;
using BusinessInterface;

namespace PartyCafe.Site.Controllers
{
    public class FoodMenuController : Controller
    {
        /*private readonly IFoodMenuServiceBase _menuService;
        public FoodMenuController(IFoodMenuServiceBase menuService)
        {
            _menuService = menuService;
        }               */

        // GET: FoodMenu
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllMenuTypes()
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetFoodMenu(int idType)
        {
            return Json(null);
        }

    }
}