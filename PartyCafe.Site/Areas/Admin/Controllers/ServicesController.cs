using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Admin/Services
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ServicesHome()
        {
            return View("ServicesHome");
        }
        [HttpGet]
        public ActionResult ServicesAdd()
        {
            return View("ServicesAdd");
        }
        [HttpGet]
        public ActionResult ServicesEdit()
        {
            return View("ServicesEdit");
        }
        [HttpGet]
        public ActionResult ServicesEditPhoto()
        {
            return View("ServicesEditPhoto");
        }

        [HttpPost]
        public string AddServices(string name, string desc)
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

                    ServiceUtils.InsertService(
                        new PCService()
                        {
                            name = name,
                            description = desc,
                            serviceType = 0
                        },
                        User.Identity.Name,
                        new PCPhoto() { data = content, fileName = filename }
                    );
                    return "ok";
                }
                else
                {
                    ServiceUtils.InsertService(
                        new PCService()
                        {
                            name = name,
                            description = desc,
                            serviceType = 0
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
        public string RemoveServices(int id)
        {
            try
            {
                ServiceUtils.DelService(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
        [HttpPost]
        public string UpdateServices(int id, string name, string desc)
        {
            try
            {
                var request = Request.Files;
                if (request.Count > 0)
                {
                    var file = Request.Files[0];

                    var content = new byte[file.ContentLength];
                    var filename = file.FileName;
                    file.InputStream.Read(content, 0, file.ContentLength);

                    ServiceUtils.EditService(
                        new PCService()
                        {
                            idRecord = id,
                            name = name,
                            description = desc
                        },
                        User.Identity.Name,
                        new PCPhoto() { data = content, fileName = filename }
                    );
                    return "ok";
                }
                else
                {
                    ServiceUtils.EditService(
                        new PCService()
                        {
                            idRecord = id,
                            name = name,
                            description = desc
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
        public string AddPhotoToServices(int id, string name, string desc)
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

                    ServiceUtils.AddPhoto(
                        id,
                        name,
                        new PCPhoto() { data = content, fileName = filename },
                        User.Identity.Name
                    );
                    return "ok";
                }
                else
                {
                    return "bed";
                }
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
        [HttpPost]
        public string UpdatePhotoServices(int id, string name)
        {
            try
            {
                ServiceUtils.EditPhoto(id, name, null, User.Identity.Name);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
        [HttpPost]
        public string RemovePhotoFromServices(int id)
        {
            try
            {
                ServiceUtils.DelPhoto(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "bed";
            }
        }

        [HttpPost]
        public JsonResult GetServicesPhotos(int id)
        {
            try
            {
                var aboutus = ServiceUtils.GetAll(ServiceType.originService).Where(w => w.idRecord == id).SingleOrDefault();
                return Json(aboutus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetAllServices()
        {
            var services = ServiceUtils.GetAll(ServiceType.originService);
            return Json(services, JsonRequestBehavior.AllowGet);
        }
    }
}