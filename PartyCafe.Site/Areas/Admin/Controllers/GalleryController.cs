using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Admin/Gallery
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GalleryHome()
        {
            return View("GalleryHome");
        }
        [HttpGet]
        public ActionResult GalleryAdd()
        {
            return View("GalleryAdd");
        }
        [HttpGet]
        public ActionResult GalleryEdit()
        {
            return View("GalleryEdit");
        }

        [HttpGet]
        public JsonResult GetAllGallery()
        {
            var gallery = GalleryUtils.GetAll();
            return Json(gallery, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string AddGallery(string name, string description)
        {
            var request = Request;
            var file = Request.Files[0];
            var content = new byte[file.ContentLength];
            var filename = file.FileName;
            file.InputStream.Read(content, 0, file.ContentLength);

            GalleryUtils.InsertGallery(
                new PCGallery(){name = name, description = description},
                User.Identity.Name,
                new PCPhoto() { data = content, fileName = filename }
            );
            return null;
        }
    }
}