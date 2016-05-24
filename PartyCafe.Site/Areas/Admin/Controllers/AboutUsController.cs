using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: Admin/AboutUs
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AboutHome()
        {
            return View("AboutHome");
        }
        [HttpGet]
        public ActionResult AboutAdd()
        {
            return View("AboutAdd");
        }
        [HttpGet]
        public ActionResult AboutEdit()
        {
            return View("AboutEdit");
        }
        [HttpGet]
        public ActionResult AboutEditPhoto()
        {
            return View("AboutEditPhoto");
        }

        [HttpGet]
        public JsonResult GetAllAbout()
        {
            var aboutus = ServiceUtils.GetAll(1);
            return Json(aboutus, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public string AddAbout(string name, string desc)
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
                            serviceType = 1
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
                            serviceType = 1
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
        public string RemoveAbout(int id)
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
        public string UpdateAbout(int id, string name, string desc)
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
        public string AddPhotoToBlock(int id, string name, string desc)
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

                    /*ServiceUtils.AddPhoto(
                        id,
                        name,
                        new PCPhoto() { data = content, fileName = filename },
                        User.Identity.Name
                    );*/
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
        public JsonResult GetBlockPhotos(int id)
        {
            try
            {
                var aboutus = ServiceUtils.GetAll(1).Where(w => w.idRecord == id).ToList();
                return Json(aboutus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public string RemovePhotoFromBlock(int id)
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
        
    }
}