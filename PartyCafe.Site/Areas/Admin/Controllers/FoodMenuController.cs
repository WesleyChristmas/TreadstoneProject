using System;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class FoodMenuController : Controller
    {
        // GET: Admin/FoodMenu
        public ActionResult Index()
        {
            return View("Index");
        }
        [HttpGet]
        public ActionResult FoodMenuHome()
        {
            return View("FoodMenuHome");
        }

        [HttpGet]
        public JsonResult GetAllMenu()
        {
            var result = MenuUtils.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}