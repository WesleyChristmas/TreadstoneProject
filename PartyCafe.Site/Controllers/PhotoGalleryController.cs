using System;
using System.Collections.Generic;
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
        public JsonResult GetAllPhotos(int startPos = 1, int count = 20)
        {
            var result = GalleryUtils.GetAll(startPos, count);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //Пока исправил на один тэг
        public JsonResult GetAllByTags(string tag, int startPos = 1, int count = 20)
        {
            var result = GalleryUtils.GetAllByTags(new List<string> {tag}, startPos, count);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GellAllHashtags()
        {
            var result = GalleryUtils.GetAllHashtags();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPhotoHashtags(int id)
        {
            var result = GalleryUtils.GetHashtagsByPhotoId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}