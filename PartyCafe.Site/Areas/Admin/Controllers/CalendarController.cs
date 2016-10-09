using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using System;
using System.Linq;
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
        public ActionResult CalendarList()
        {
            return View("CalendarList");
        }

        [HttpGet]
        public ActionResult CalendarNew()
        {
            return View("CalendarNew");
        }

        [HttpGet]
        public ActionResult CalendarEdit()
        {
            return View("CalendarEdit");
        }


        [HttpGet]
        public JsonResult GetAllEvents()
        {
            return Json(EventUtils.GetAll().OrderByDescending(x=>x.DateEvent), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEvent(int id)
        {
            return Json(EventUtils.GetAll().FirstOrDefault(x=>x.idRecord == id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEventPhotos(int id)
        {
            return Json(EventUtils.GetEventPhotos(id));
        }

        [HttpPost]
        public string AddPhoto(int idEvent, string name)
        {
            try
            {
                EventUtils.AddPhoto(idEvent, name, ControllerUtils.GetPhotoEntity(Request.Files), User.Identity.Name);
                return "ok";
            }
            catch (Exception ex)
            {
                return "bad";
            }
        }

        [HttpPost]
        public string DelPhoto(int idEvent)
        {
            try
            {
                EventUtils.DelPhoto(idEvent);
                return "ok";
            }
            catch (Exception ex)
            {
                return "bad";
            }
        }

        [HttpPost]
        public string AddCalendarEvent(string name, string desc, string date, string time, bool isOpen)
        {
            try
            {
                EventUtils.InsertEvent(
                    new PCEvent()
                    {
                        name = name,
                        Description = desc,
                        DateEvent = DateTime.Parse(date),
                        TimeEvent = TimeSpan.Parse(time),
                        IsOpen = isOpen
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
        public string UpdateCalendarEvent(int id, string name, string desc, string date, string time, bool isOpen)
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
                            TimeEvent = TimeSpan.Parse(time),
                            IsOpen = isOpen
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

        [HttpGet]
        public string RemoveCalendarEvent(int id)
        {
            try
            {
                EventUtils.DelEvent(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

    }
}