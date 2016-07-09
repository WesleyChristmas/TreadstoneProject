using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using System;

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

        [HttpPost]
        public string AddCalendarEvent(string name, string desc, string date, string time)
        {
            try
            {
                var request = Request;
                if (request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    var content = new byte[file.ContentLength];
                    var filename = file.FileName;
                    file.InputStream.Read(content, 0, file.ContentLength);

                    EventUtils.InsertEvent(
                        new PCEvent()
                        {
                            name = name,
                            Description = desc,
                            DateEvent = DateTime.Parse(date),
                            TimeEvent = TimeSpan.Parse(time)
                        },
                        User.Identity.Name,
                        new PCPhoto() { data = content, fileName = filename }
                    );
                    return "ok";
                }
                else
                {
                    EventUtils.InsertEvent(
                        new PCEvent()
                        {
                            name = name,
                            Description = desc,
                            DateEvent = DateTime.Parse(date),
                            TimeEvent = TimeSpan.Parse(time)
                        },
                        User.Identity.Name,
                        null
                    );
                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
        [HttpPost]
        public string UpdateCalendarEvent(int id, string name, string desc, string date, string time)
        {
            try
            {
                var request = Request.Files;
                if (request.Count > 0)
                {
                    var file = Request.Files[0];

                    var content = new byte[file.ContentLength];
                    var filename = file.FileName;
                    file.InputStream.Read(content, 0, file.ContentLength);

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
                        new PCPhoto() { data = content, fileName = filename }
                    );
                    return "ok";
                }
                else
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
                        null
                    );
                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
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