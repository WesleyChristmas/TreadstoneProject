using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using PartyCafe.Site.DBUtils;
using System.Net.Mail;

namespace PartyCafe.Site.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
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
            var result = ServiceUtils.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> NewOrder(string user, string phone, string date, string time, string service)
        {
            var mess = "Заказ услуги - " + service + "\n";
            mess += " Имя:" + user + "\n";
            mess += " Телефона:" + phone + "\n";
            mess += " Желаемая дата:" + date + "\n";
            mess += " Желаемое время:" + time + "\n";

            try
            {
                MailMessage mm = new MailMessage("igdevelopment-info@yandex.ru", "dropletofrain@mail.ru");
                mm.Subject = "Сообщение с контактной формы";
                mm.Body = mess;

                SmtpClient sc = new SmtpClient("smtp.yandex.ru", 25);
                sc.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "igdevelopment-info@yandex.ru",
                    Password = "dev89%lopment"
                };
                sc.EnableSsl = true;
                //sc.Send(mm);

                return Json("good", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
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