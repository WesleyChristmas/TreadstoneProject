﻿using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;


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
            EventResult er = new EventResult
            {
                Calendar = EventUtils.GetNearEvents(),
                CurDate = DateTime.Now
            };

            return Json(er, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string Invite(string username, string phone, int? peopleNum, string promoCode, string service)
        {
            try
            { 
                var subject = "Заказ услуги с сайта PartyCafe";

                var message = "Заказ услуги - " + service + Environment.NewLine +
                    "Имя: " + username + Environment.NewLine +
                    "Телефона:" + phone + Environment.NewLine +
                    ((peopleNum.HasValue) ? "Количество человек: " + peopleNum + Environment.NewLine : string.Empty) +
                    "Промокод:" + promoCode;

                var reader = new System.Configuration.AppSettingsReader();
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