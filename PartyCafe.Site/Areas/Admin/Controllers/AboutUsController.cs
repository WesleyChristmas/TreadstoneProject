using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using PartyCafe.Site.Models.Utils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [Authorize]
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
                ServiceUtils.InsertService(
                    new PCService()
                    {
                        name = name,
                        description = desc,
                        serviceType = 1
                    },
                    User.Identity.Name,
                    ControllerUtils.GetPhotoEntity(Request.Files)
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
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
                return "Произошла ошибка! " + ex.Message;
            }
        }
        [HttpPost]
        public string UpdateAbout(int id, string name, string desc)
        {
            try
            {
                ServiceUtils.EditService(
                        new PCService()
                        {
                            idRecord = id,
                            name = name,
                            description = desc
                        },
                        ControllerUtils.GetPhotoEntity(Request.Files),
                        User.Identity.Name
                    );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }

        [HttpPost]
        public string AddPhotoToBlock(int id, string name)
        {
            try
            {
                var photo = ControllerUtils.GetPhotoEntity(Request.Files);
                if (photo == null) return "bad";

                ServiceUtils.AddPhoto(
                    id,
                    name,
                    photo,
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

        [HttpPost]
        public string UpdatePhotoBlock(int id, string name)
        {
            try
            {
                ServiceUtils.EditPhoto(id, name, ControllerUtils.GetPhotoEntity(Request.Files), User.Identity.Name);
                return "ok";
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
                var aboutus = ServiceUtils.GetServicePhotos(id);
                return Json(aboutus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}