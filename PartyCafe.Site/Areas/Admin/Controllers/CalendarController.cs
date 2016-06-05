using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Admin/Calendar
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CalendarHome()
        {
            return View("CalendarHome");
        }
        [HttpGet]
        public ActionResult CalendarAdd()
        {
            return View("CalendarAdd");
        }
        [HttpGet]
        public ActionResult CalendarEdit()
        {
            return View("CalendarEdit");
        }


        [HttpGet]
        public JsonResult GetAllEvents()
        {
            return Json(EventUtils.GetAll(), JsonRequestBehavior.AllowGet);
        }

        /*
        [HttpPost]
        public void AddBlogCalendar(BlogCalendarEntity calendar)
        {
           // _calendarService.AddBlogCalendar(calendar, ImageSaver.GetSingleImage(Request, 2, HttpContext.Server.MapPath("/")));
        }
        */

        [HttpPost]
        public void UpdateBlogCalendar(PCEvent calendar)
        {

            EventUtils.EditEvent(calendar, "admin", null);
            //_calendarService.UpdateBlogCalendar(calendar, ImageSaver.GetSingleImage(Request, 2, HttpContext.Server.MapPath("/")));
        }

        [HttpPost]
        public JsonResult DeleteBlogCalendar(int idCalendar)
        {
            //return Json(_calendarService.DeleteBlogCalendar(idCalendar, HttpContext.Server.MapPath("/")));
            return null;
        }

    }
}