using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllServices()
        {
            var result = ServiceUtils.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}