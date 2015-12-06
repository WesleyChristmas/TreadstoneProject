using System.Web.Mvc;
using BusinessEntity;
using BusinessInterface;
using PartyCafe.Site.Areas.Admin.Core;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class FoodMenuController : Controller
    {
        private readonly IFoodMenuServiceBase _menuService;

        public FoodMenuController(IFoodMenuServiceBase menuService)
        {
            _menuService = menuService;
        }

        // GET: Admin/FoodMenu
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult MenuSections()
        {
            return View("MenuSections");
        }

        [HttpGet]
        public ActionResult EditMenuSection()
        {
            return View("EditMenuSection");
        }

        [HttpGet]
        public ActionResult MenuItems()
        {
            return View("FoodMenuItems");
        }

        [HttpGet]
        public JsonResult GetAllMenuTypes()
        {
            return Json(_menuService.GetAllFoodMenuTypes(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddMenuType(FoodMenuTypeEntity type)
        {
            _menuService.AddFoodMenuType(type, ImageSaver.GetSingleImage(Request, 1, HttpContext.Server.MapPath("/")));
        }

        [HttpPost]
        public void UpdateMenuType(FoodMenuTypeEntity type)
        {
            _menuService.UpdateMenuType(type, ImageSaver.GetSingleImage(Request, 1, HttpContext.Server.MapPath("/")));
        }

        [HttpPost]
        public JsonResult DeleteMenuType(int idType)
        {
            return Json(_menuService.DeleteMenuType(idType, HttpContext.Server.MapPath("/")), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetFoodMenu(int idType)
        {
            return Json(_menuService.GetFoodMenu(idType));
        }

        [HttpPost]
        public void AddFoodMenu(FoodMenuEntity item)
        {
            _menuService.AddFoodMenu(item, ImageSaver.GetSingleImage(Request, 2, HttpContext.Server.MapPath("/")));
        }

        [HttpPost]
        public void UpdateFoodMenu(FoodMenuEntity item)
        {
            _menuService.UpdateFoodMenu(item, ImageSaver.GetSingleImage(Request, 2, HttpContext.Server.MapPath("/")));
        }

        [HttpPost]
        public JsonResult DeleteFoodMenu(int idMenuItem)
        {
            return Json(_menuService.DeleteFoodMenu(idMenuItem, HttpContext.Server.MapPath("/")));
        }
    }
}