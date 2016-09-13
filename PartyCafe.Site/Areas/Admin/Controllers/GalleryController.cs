using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public JsonResult GetAllGallery(int startPos = 1, int count = 20)
        {
            var gallery = GalleryUtils.GetAll(startPos, count);
            return Json(gallery, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllGalleryByTags(List<string> hashtags, int startPos = 1, int count = 20)
        {
            var gallery = GalleryUtils.GetAllByTags(hashtags, startPos, count);
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
        public JsonResult GetHashtags(int id)
        {
            return Json(GalleryUtils.GetHashtagsByPhotoId(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string SetHashtags(int id, List<string> hashtags)
        {
            try
            {
                GalleryUtils.SetHashtags(id, hashtags);
                return "ok";
            }
            catch (Exception ex)
            {
                return "bad";
            }
        }
    }
}