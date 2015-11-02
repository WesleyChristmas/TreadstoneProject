using System.Web.Mvc;
using BusinessEntity;
using Db.Service;
using PartyCafe.Site.Areas.Admin.Core;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IBlogCalendarService _calendarService;

        public CalendarController(IBlogCalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        // GET: Admin/Calendar
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CalendarEvents()
        {
            return View("CalendarEvents");
        }

        [HttpGet]
        public ActionResult EditCalendarEvent()
        {
            return View("EditCalendarEvent");
        }

        [HttpGet]
        public JsonResult GetCalendar()
        {
            return Json(_calendarService.GetCalendar(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddBlogCalendar(BlogCalendarEntity calendar)
        {
            _calendarService.AddBlogCalendar(calendar, ImageSaver.GetSingleImage(Request));
        }

        [HttpPost]
        public void EditBlogCalendar(BlogCalendarEntity calendar)
        {
            _calendarService.EditBlogCalendar(calendar, ImageSaver.GetSingleImage(Request));
        }

        [HttpPost]
        public JsonResult DeleteBlogCalendar(int idCalendar)
        {
           return Json(_calendarService.DeleteBlogCalendar(idCalendar));
        }

    }
}