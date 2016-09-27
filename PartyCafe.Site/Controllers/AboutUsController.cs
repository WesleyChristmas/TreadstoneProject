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
        public ActionResult ServiceList()
        {
            return View("ServiceList");
        }

        [HttpGet]
        public ActionResult ServiceDetailed()
        {
            return View("ServiceDetailed");
        }


        [HttpGet]
        public JsonResult GetAllUs()
        {
            var result = AboutUtils.GetAbout();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}