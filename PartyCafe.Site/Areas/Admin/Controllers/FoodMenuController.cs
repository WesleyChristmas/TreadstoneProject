using System.Web.Mvc;
using BusinessEntity;
using BusinessEntity.Models;
using BusinessInterface;

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
            ReceiveFileModel image = null;
            if (Request.Files.Count == 1)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, file.ContentLength);

                    image = new ReceiveFileModel
                    {
                        Data = content,
                        FileName = file.FileName,
                        IdSection = 1
                    };
                }
            }
            _menuService.AddFoodMenuType(type,image);
        }

        [HttpPost]
        public void UpdateMenuType(FoodMenuTypeEntity type)
        {
            ReceiveFileModel image = null;
            if (Request.Files.Count == 1)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, file.ContentLength);

                    image = new ReceiveFileModel
                    {
                        Data = content,
                        FileName = file.FileName,
                        IdSection = 1
                    };
                }
            }
            _menuService.UpdateMenuType(type, image);
        }

        [HttpPost]
        public JsonResult DeleteMenuType(int idType)
        {
            return Json(_menuService.DeleteMenuType(idType), JsonRequestBehavior.AllowGet);
        }
    }
}