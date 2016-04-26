using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mail;

namespace PartyCafe.Site.Controllers
{
    public class EventCalendarController : Controller
    {
        private class EventResult
        {
            public DateTime CurDate;
            public List<PCEvent> Calendar;
        } 

        // GET: EventCalendar
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Calendar()
        {
            return View("Calendar");
        }

        [HttpGet]
        public ActionResult Invite()
        {
            return View("Invite");
        }

        [HttpGet]
        public JsonResult GetCalendar()
        {
            EventResult er = new EventResult();
            er.Calendar = EventUtils.GetNearEvents();
            er.CurDate = DateTime.Now;
            
            return Json(er, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Invite(string user, string phone, int? people, string promo, string service)
        {
            var mess = "Заказ услуги - " + service + "\n";
            mess += " Имя:" + user + "\n";
            mess += " Телефона:" + phone + "\n";
            mess += " Количество человек:" + people + "\n";
            mess += " Промокод:" + promo + "\n";

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
    }
}