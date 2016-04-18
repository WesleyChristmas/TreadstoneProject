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
            // тут можно сделать тесты всех классов
            //TestAllClasses();
            return View();
        }

        private PCPhoto GetPhoto1()
        {
            var data = System.IO.File.ReadAllBytes(@"C:\Git\PartyCafe\PartyCafe.Site\Content\images\gallery\1.jpg");
            PCPhoto p = new PCPhoto();
            p.data = data;
            p.fileName = "TEST.JPG";
            return p;
        }

        private PCPhoto GetPhoto2()
        {
            var dir = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            var data = System.IO.File.ReadAllBytes(dir + @"\Content\images\gallery\6.jpg");
            var p = new PCPhoto();
            p.data = data;
            p.fileName = "TEST2.JPG";
            return p;
        }

        private void TestEvents()
        {
            var events = EventUtils.GetAll();
            var count = events.Count;

            var ev = new PCEvent();
            ev.name = "Тестовое событие";
            ev.DateEvent = DateTime.Now;

            EventUtils.InsertEvent(ev, "TEST-USER", GetPhoto1());

            events = EventUtils.GetAll();
            foreach (var c in events)
            {
                if (c.name == "Тестовое событие")
                {
                    c.name = "Измененное сообщение";
                    EventUtils.EditEvent(c, "TEST-USER", GetPhoto2());
                    EventUtils.DelEvent(c.idRecord);
                }
            }
        }

        private void TestAllClasses()
        {
            // Тестирование всех классов
            TestEvents();
            TestGames();
            TestGallery();
            TestMenu();
        }

        private void TestMenu()
        {
            var x = MenuUtils.GetAll();
        }

        private void TestGallery()
        {
            var gallery = GalleryUtils.GetAll();
            var count = gallery.Count;

            var gal = new PCGallery();
            gal.name = "Тестовый элемент галлереи";
            gal.description = "123312";

            GalleryUtils.InsertGallery(gal, "TEST-USER", GetPhoto1());

            gallery = GalleryUtils.GetAll();
            foreach (var c in gallery)
            {
                if (c.name == "Тестовый элемент галлереи")
                {
                    c.name = "Тестовый элемент галлереи123";
                    GalleryUtils.EditGallery(c, "TEST-USER", GetPhoto2());
                    GalleryUtils.DelGallery(c.idRecord);
                }
            }
        }

        private void TestGames()
        {
            var games = GameUtils.GetAll();
            var count = games.Count;

            var game = new PCGame();
            game.name = "Тестовая игра";
            game.description = "222333444";

            GameUtils.InsertGame(game, "TEST-USER", GetPhoto1());

            games = GameUtils.GetAll();
            foreach (var c in games)
            {
                if (c.name == "Тестовая игра")
                {
                    c.name = "Тестовая игра123";
                    GameUtils.EditGame(c, "TEST-USER", GetPhoto2());
                    GameUtils.DelGame(c.idRecord);
                }
            }
        }

        [HttpGet]
        public JsonResult GetAllPhotos()
        {
            var result = GalleryUtils.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}