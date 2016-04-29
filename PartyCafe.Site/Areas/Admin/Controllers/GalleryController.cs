﻿using System;
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
        public string AddGallery(string name, string desc)
        {
            try
            {
                var request = Request;
                var file = Request.Files[0];
                var content = new byte[file.ContentLength];
                var filename = file.FileName;
                file.InputStream.Read(content, 0, file.ContentLength);

                GalleryUtils.InsertGallery(
                    new PCGallery() { name = name, description = desc },
                    User.Identity.Name,
                    new PCPhoto() { data = content, fileName = filename }
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }

        [HttpPost]
        public string UpdateGallery(int id, string name, string desc)
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

                    GalleryUtils.EditGallery(
                        new PCGallery()
                        {
                            idRecord = id,
                            name = name,
                            description = desc
                        },
                        User.Identity.Name,
                        new PCPhoto() { data = content, fileName = filename }
                    );
                    return "ok";
                }
                else
                {
                    GalleryUtils.EditGallery(
                        new PCGallery()
                        {
                            idRecord = id,
                            name = name,
                            description = desc
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
    }
}