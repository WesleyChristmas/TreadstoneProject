using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


    }
}