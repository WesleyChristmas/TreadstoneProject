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
            return View();
        }

        [HttpGet]
        public JsonResult GetAllPhotos()
        {
            var result = GalleryUtils.GetAll();
            result.ForEach(f => f.photoPath.Replace("\\", "/"));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}