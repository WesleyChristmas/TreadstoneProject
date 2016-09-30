﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyCafe.Site.DBUtils;
using PartyCafe.Site.Models.Utils;
using System.Configuration;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class AboutUsController : Controller
    {
        // GET: Admin/AboutUs
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AboutHome()
        {
            return View("AboutHome");
        }
        [HttpGet]
        public ActionResult AboutAdd()
        {
            return View("AboutAdd");
        }
        [HttpGet]
        public ActionResult AboutEdit()
        {
            return View("AboutEdit");
        }
        [HttpGet]
        public ActionResult AboutEditPhoto()
        {
            return View("AboutEditPhoto");
        }

        [HttpPost]
        public string AddServices(string name, string desc)
        {
            try
            {
                ServiceUtils.InsertService(
                    new PCService()
                    {
                        name = name,
                        description = desc,
                        serviceType = ServiceType.aboutService
                    },
                    User.Identity.Name,
                    ControllerUtils.GetPhotoEntity(Request.Files)
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }

        [HttpPost]
        public string RemoveServices(int id)
        {
            try
            {
                ServiceUtils.DelService(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message.ToString();
            }
        }
        [HttpPost]
        public string UpdateServices(int id, string name, string desc)
        {
            try
            {
                ServiceUtils.EditService(
                    new PCService()
                    {
                        idRecord = id,
                        name = name,
                        description = desc
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
        public string AddPhotoToServices(int id, string name)
        {
            try
            {
                var photo = ControllerUtils.GetPhotoEntity(Request.Files);
                if (photo == null) return "bad";

                ServiceUtils.AddPhoto(
                    id,
                    name,
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
        public string UpdatePhotoServices(int id, string name)
        {
            try
            {
                ServiceUtils.EditPhoto(id, name, ControllerUtils.GetPhotoEntity(Request.Files), User.Identity.Name);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        [HttpPost]
        public string RemovePhotoFromServices(int id)
        {
            try
            {
                ServiceUtils.DelPhoto(id);
                return "ok";
            }
            catch (Exception ex)
            {
                return "bad";
            }
        }

        [HttpPost]
        public JsonResult GetServicesFull(int id)
        {
            try
            {
                var aboutus = ServiceUtils.GetServiceFull(id);
                return Json(aboutus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAllServices()
        {
            var services = ServiceUtils.GetAll(ServiceType.originService);
            return Json(services, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string AddVideoToServices(int serviceId, string name, string description, string url)
        {
            try
            {
                ServiceUtils.AddVideo(
                    serviceId,
                    name,
                    description,
                    url
                );
                return "ok";
            }
            catch (Exception ex)
            {
                return "Произошла ошибка! " + ex.Message;
            }
        }

        [HttpPost]
        public string RemoveVideoFromServices(int serviceId)
        {
            try
            {
                ServiceUtils.DelVideo(serviceId);
                return "ok";
            }
            catch (Exception ex)
            {
                return "bad";
            }
        }
    }
}