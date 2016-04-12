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
        // GET: AboutUs
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllUs()
        {
            var result = ServiceUtils.GetAll(1);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}