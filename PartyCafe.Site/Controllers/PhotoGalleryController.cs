using System;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Controllers
{
    public class PhotoGalleryController : Controller
    {
        // GET: PhotoGallery
        public ActionResult Index()
        {
            TestAllClasses();
            return View();
        }

        private void TestAllClasses()
        {
            var events = EventUtils.GetAll();
            var count = events.Count;

            var ev = new PCEvent();
            ev.name = "Тестовое событие";
            ev.DateEvent = DateTime.Now;

            var data = System.IO.File.ReadAllBytes(@"\Git\PartyCafe\PartyCafe.Site\Content\images\gallery\1.jpg");
            PCPhoto p = new PCPhoto();
            p.data = data;
            p.fileName = "TEST.JPG";
            EventUtils.InsertEvent(ev, "TEST-USER", p);

            
        }
    }
}