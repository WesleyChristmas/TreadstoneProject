using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using PartyCafe.Site.Models.Utils;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        // GET: Admin/Gallery
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllGallery(int startPos = 1, int count = 20)
        {
            var gallery = GalleryUtils.GetAll(startPos, count);
            return Json(gallery, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllGalleryByTags(string tag, int startPos = 1, int count = 20)
        {
            var gallery = GalleryUtils.GetAllByTags(new List<string> {tag}, startPos, count);
            return Json(gallery, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string AddGallery(string name, string desc)
        {
            try
            {
                var photo = ControllerUtils.GetPhotoEntity(Request.Files);
                if (photo == null) return "bad";

                GalleryUtils.InsertGallery(
                    new PCGallery() {name = name, description = desc},
                    photo,
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
        public string UpdateGallery(int id, string name, string desc)
        {
            try
            {
                var photo = ControllerUtils.GetPhotoEntity(Request.Files);
                if (photo == null) return "bad";

                GalleryUtils.EditGallery(
                    new PCGallery()
                    {
                        idRecord = id,
                        name = name,
                        description = desc
                    },
                    photo,
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
        public string DeleteGalleryItem(int id)
        {
            try
            {
                GalleryUtils.DelGallery(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        [HttpGet]
        public JsonResult GellAllHashtags()
        {
            var result = GalleryUtils.GetAllHashtags();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetHashtags(int id)
        {
            return Json(GalleryUtils.GetHashtagsByPhotoId(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string SetHashtag(int id, string hashtag)
        {
            try
            {
                GalleryUtils.SetHashtags(id, new List<string>{hashtag});
                return "ok";
            }
            catch (Exception ex)
            {
                return "bad";
            }
        }
    }
}