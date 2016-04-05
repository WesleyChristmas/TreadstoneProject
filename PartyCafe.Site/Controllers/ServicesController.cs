using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<JsonResult> NewOrder(string user, string phone, string date, string time)
        {
            return null;
        }

        public class Order
        {
            string user { get; set; }
            string phone { get; set; }
            string date { get; set; }
            string time { get; set; }
        }
    }
}