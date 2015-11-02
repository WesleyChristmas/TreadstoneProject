using System.Web.Mvc;
using BusinessEntity;
using BusinessInterface;
using PartyCafe.Site.Areas.Admin.Core;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [Authorize]
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
        public JsonResult GetAllMenuTypes()
        {
            return Json(_menuService.GetAllFoodMenuTypes(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddMenuType(FoodMenuTypeEntity type)
        {
            _menuService.AddFoodMenuType(type, ImageSaver.GetSingleImage(Request));
        }

        [HttpPost]
        public void UpdateMenuType(FoodMenuTypeEntity type)
        {
            _menuService.UpdateMenuType(type, ImageSaver.GetSingleImage(Request));
        }

        [HttpPost]
        public JsonResult DeleteMenuType(int idType)
        {
            return Json(_menuService.DeleteMenuType(idType), JsonRequestBehavior.AllowGet);
        }
    }
}