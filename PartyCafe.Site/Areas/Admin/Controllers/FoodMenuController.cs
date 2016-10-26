using System;
using System.Linq;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using PartyCafe.Site.Models.Utils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class FoodMenuController : Controller
    {
        // GET: Admin/FoodMenu
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult MenuList()
        {
            return View("MenuList");
        }


        [HttpGet]
        public ActionResult MenuItems()
        {
            return View("MenuItems");
        }

        [HttpGet]
        public ActionResult MenuItemEdit()
        {
            return View("MenuItemEdit");
        }

        [HttpGet]
        public ActionResult MenuItemNew()
        {
            return View("MenuItemNew");
        }

        [HttpGet]
        public ActionResult MenuNew()
        {
            return View("MenuNew");
        }

        [HttpGet]
        public ActionResult MenuEdit()
        {
            return View("MenuEdit");
        }

        [HttpGet]
        public JsonResult GetAllMenu()
        {
            var result = MenuUtils.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMenuList()
        {
            var result = MenuUtils.GetAll().Select(x =>
                new
                {
                    x.idRecord,
                    x.name,
                    x.photoPath
                });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMenu(int menuId)
        {
            var result = MenuUtils.GetAll().Select(x =>
                new
                {
                    x.idRecord,
                    x.name,
                    x.photoPath
                }).FirstOrDefault(x => x.idRecord == menuId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMenuSubList(int listId)
        {
            var result = MenuUtils.GetAll().Where(x => x.idRecord == listId).Select(x => new
            {
                x.name,
                subGroups = x.subGroups.Select(k => new
                {
                    k.idRecord,
                    k.name,
                    k.photoPath
                })
            }).FirstOrDefault();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMenuSub(int submenuId)
        {
            var result = MenuUtils.GetAll().Where(x => x.subGroups.Any(k => k.idRecord == submenuId))
                .SelectMany(x => x.subGroups).FirstOrDefault(x => x.idRecord == submenuId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMenuItems(int menuId)
        {
            var result =
                MenuUtils.GetAll().FirstOrDefault(x => x.idRecord == menuId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMenuItem(int itemId)
        {
            var result =
                MenuUtils.GetAll().SelectMany(x => x.items).FirstOrDefault(x => x.idRecord == itemId);
                    
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string AddGroupItem(string name, int? parentId = null)
        {
            try
            {
                MenuUtils.InsertGroup(
                    new PCMenuGroup()
                    {
                        IdParent = parentId,
                        Name = name
                    },
                    ControllerUtils.GetPhotoEntity(Request.Files),
                    User.Identity.Name
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }
        [HttpPost]
        public string EditGroupItem(string name, int id, int? idparent = null)
        {
            try
            {
                MenuUtils.EditGroup(
                    new PCMenuGroup()
                    {
                        IdRecord = id,
                        Name = name,
                        IdParent = idparent
                    },
                    ControllerUtils.GetPhotoEntity(Request.Files),
                    User.Identity.Name
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }
        [HttpGet]
        public string RemoveGroupItem(int id)
        {
            try
            {
                MenuUtils.DelGroup(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        [HttpPost]
        public string AddItem(string name, int menuId, string des, string weipla, float price)
        {
            try
            {
                MenuUtils.InsertItem(
                        new PCMenuItem
                        {
                            IdGroup = menuId,
                            Name = name,
                            Description = des,
                            Weight = weipla,
                            Platform = weipla,
                            Price = price,
                            Country = des
                        },
                        ControllerUtils.GetPhotoEntity(Request.Files),
                        User.Identity.Name
                    );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        //string name, int menuId, string des, string weipla, float price, int idrecord
        [HttpPost]
        public string EditItem(int menuId,string name,string des, string weipla,float price,int idrecord)
        {
            try
            {
                MenuUtils.EditItem(
                        new PCMenuItem
                        {
                            IdGroup = menuId,
                            IdRecord = idrecord,
                            Name = name,
                            Description = des,
                            Weight = weipla,
                            Platform = weipla,
                            Price = price,
                            Country = des
                        },
                        ControllerUtils.GetPhotoEntity(Request.Files),
                        User.Identity.Name
                    );
                    return "ok";
            }
            catch ( Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        [HttpGet]
        public string RemoveItem(int id)
        {
            try
            {
                MenuUtils.DelItem(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }
    }
}