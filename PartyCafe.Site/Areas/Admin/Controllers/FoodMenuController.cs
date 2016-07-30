using System;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

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
        [HttpPost]
        public string RemoveGroupItem(int id)
        {
            try
            {
                MenuUtils.DelGroup(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }

        [HttpPost]
        public string AddItem(string name, int groupid, string des, string weipla, float price)
        {
            try
            {
                var request = Request;
                if (request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    var content = new byte[file.ContentLength];
                    var filename = file.FileName;
                    file.InputStream.Read(content, 0, file.ContentLength);

                    MenuUtils.InsertItem(
                        new PCMenuItem()
                        {
                            IdGroup = groupid,
                            Name = name,
                            Description = des,
                            Weight = weipla,
                            Platform = weipla,
                            Price = price,
                            Country = des
                        },
                        User.Identity.Name,
                        new PCPhoto() { data = content, fileName = filename }
                    );
                    return "ok";
                }
                else {
                    MenuUtils.InsertItem(
                        new PCMenuItem()
                        {
                            IdGroup = groupid,
                            Name = name,
                            Description = des,
                            Weight = weipla,
                            Platform = weipla,
                            Price = price,
                            Country = des
                        },
                        User.Identity.Name,
                        null
                    );
                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
        [HttpPost]
        public string EditItem(string name, int groupid, string des, string weipla, float price, int idrecord)
        {
            try
            {
                var request = Request;
                if (request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    var content = new byte[file.ContentLength];
                    var filename = file.FileName;
                    file.InputStream.Read(content, 0, file.ContentLength);

                    MenuUtils.EditItem(
                        new PCMenuItem()
                        {
                            IdGroup = groupid,
                            IdRecord = idrecord,
                            Name = name,
                            Description = des,
                            Weight = weipla,
                            Platform = weipla,
                            Price = price,
                            Country = des
                        },
                        User.Identity.Name,
                        new PCPhoto() { data = content, fileName = filename }
                    );
                    return "ok";
                }
                else
                {
                    MenuUtils.EditItem(
                        new PCMenuItem()
                        {
                            IdGroup = groupid,
                            IdRecord = idrecord,
                            Name = name,
                            Description = des,
                            Weight = weipla,
                            Platform = weipla,
                            Price = price,
                            Country = des
                        },
                        User.Identity.Name,
                        null
                    );
                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
        [HttpPost]
        public string RemoveItem(int id)
        {
            try
            {
                MenuUtils.DelItem(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
    }
}