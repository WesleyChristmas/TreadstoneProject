using System.Web.Mvc;
using BusinessEntity;
using Db.Service;
using PartyCafe.Site.DBUtils;
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
            return Json(EventUtils.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddBlogCalendar(BlogCalendarEntity calendar)
        {
            _calendarService.AddBlogCalendar(calendar, ImageSaver.GetSingleImage(Request, 2, HttpContext.Server.MapPath("/")));
        }

        [HttpPost]
        public void UpdateBlogCalendar(BlogCalendarEntity calendar)
        {
            _calendarService.UpdateBlogCalendar(calendar, ImageSaver.GetSingleImage(Request, 2, HttpContext.Server.MapPath("/")));
        }

        [HttpPost]
        public JsonResult DeleteBlogCalendar(int idCalendar)
        {
            return Json(_calendarService.DeleteBlogCalendar(idCalendar, HttpContext.Server.MapPath("/")));
        }

    }
}