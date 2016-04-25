using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using System;
using System.Collections.Generic;

namespace PartyCafe.Site.Controllers
{
    public class EventCalendarController : Controller
    {
        private class EventResult
        {
            public DateTime CurDate;
            public List<PCEvent> Calendar;
        } 

        /*private readonly IBlogCalendarService _calendarService;
        public EventCalendarController(IBlogCalendarService calendarService)
        {
            _calendarService = calendarService;
        }*/

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
    }
}