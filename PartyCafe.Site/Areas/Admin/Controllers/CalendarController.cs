using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using System;
using PartyCafe.Site.Models.Utils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [Authorize]
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

        [HttpPost]
        public string AddCalendarEvent(string name, string desc, string date, string time)
        {
            try
            {
                EventUtils.InsertEvent(
                    new PCEvent()
                    {
                        name = name,
                        Description = desc,
                        DateEvent = DateTime.Parse(date),
                        TimeEvent = TimeSpan.Parse(time)
                    },
                    ControllerUtils.GetPhotoEntity(Request.Files),
                    User.Identity.Name
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        [HttpPost]
        public string UpdateCalendarEvent(int id, string name, string desc, string date, string time)
        {
            try
            {
                EventUtils.EditEvent(
                        new PCEvent()
                        {
                            idRecord = id,
                            name = name,
                            Description = desc,
                            DateEvent = DateTime.Parse(date),
                            TimeEvent = TimeSpan.Parse(time)
                        },
                        User.Identity.Name,
                        ControllerUtils.GetPhotoEntity(Request.Files)
                    );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        [HttpPost]
        public string RemoveCalendarEvent(int id)
        {
            try
            {
                EventUtils.DelEvent(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }

    }
}