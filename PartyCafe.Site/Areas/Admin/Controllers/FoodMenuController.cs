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

        [HttpPost]
        public string AddMainItem(string name)
        {
            try
            {
                MenuUtils.InsertGroup(
                    new PCMenuGroup()
                    {
                        IdParent = null,
                        Name = name
                    },
                    User.Identity.Name,
                    null
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
    }
}