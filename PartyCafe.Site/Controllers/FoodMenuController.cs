using System.Web.Mvc;
using System.Collections.Generic;
using PartyCafe.Site.DBUtils;

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
        public JsonResult GetAllMenu()
        {
            var result = MenuUtils.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFoodMenu(int idType)
        {
            return Json(null);
        }

    }
}