using System.Web.Mvc;
using Db.Service;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Controllers
{
    public class EventCalendarController : Controller
    {
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
        public JsonResult GetCalendar()
        {
            return Json(EventUtils.GetNearEvents(), JsonRequestBehavior.AllowGet);
        }
    }
}