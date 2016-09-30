using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using System.Threading.Tasks;
using System.Configuration;

namespace PartyCafe.Site.Controllers
{
    public class AboutUsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServiceList()
        {
            return View("ServiceList");
        }

        public ActionResult ServiceDetailed()
        {
            return View("ServiceDetailed");
        }

        [HttpGet]
        public JsonResult GetAllServices()
        {
            var result = ServiceUtils.GetAll(ServiceType.aboutService);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetServiceFull(int serviceId)
        {
            var result = ServiceUtils.GetServiceFull(serviceId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string NewOrder(string username, string phone, int? peopleNum, string promoCode, string service)
        {
            try
            {
                var subject = "Заказ услуги с сайта PartyCafe";

                var message = "Заказ услуги - " + service + Environment.NewLine +
                    "Имя: " + username + Environment.NewLine +
                    "Телефон:" + phone + Environment.NewLine +
                    ((peopleNum.HasValue) ? "Количество человек: " + peopleNum + Environment.NewLine : string.Empty) +
                    "Промокод:" + promoCode;

                var reader = new AppSettingsReader();
                var recipient = reader.GetValue("EmailOrdersTo", typeof(string)).ToString();

                EmailUtils.AddEmail(subject, message, recipient);


                return "ok";
            }
            catch (Exception ex)
            {
                return "bad";
            }
        }
    }
}