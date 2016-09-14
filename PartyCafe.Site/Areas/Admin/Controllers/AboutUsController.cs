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
            var aboutus = AboutUtils.GetAbout();
            return Json(aboutus, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string UpdateData(string name, string desc)
        {
            try
            {
                AboutUtils.UpdateAbout(
                    new AboutData()
                    {
                        Name = name,
                        Description = desc,
                    }
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        [HttpPost]
        public string UpdateMainPhoto()
        {
            try
            {
                AboutUtils.UpdateMainPhoto(ControllerUtils.GetPhotoEntity(Request.Files));
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
    }
}