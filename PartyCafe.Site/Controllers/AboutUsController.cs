using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Controllers
{
    public class AboutUsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Home()
        {
            return View("Home");
        }

        [HttpGet]
        public ActionResult Current()
        {
            return View("Current");
        }

        [HttpGet]
        public JsonResult GetAllUs()
        {
            var result = ServiceUtils.GetAll(1);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPhotos(int id)
        {
            var result = ServiceUtils.GetServicePhotos(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}