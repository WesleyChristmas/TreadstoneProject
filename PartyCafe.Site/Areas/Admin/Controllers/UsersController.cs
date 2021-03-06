﻿using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using PartyCafe.Site.Areas.Admin.Core.Utils;
using PartyCafe.Site.Models;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UsersList()
        {
            return View("UsersList");
        }
        [HttpGet]
        public ActionResult UsersNew()
        {
            return View("UsersNew");
        }
        [HttpGet]
        public ActionResult UsersEdit()
        {
            return View("UsersEdit");
        }

        [HttpGet]
        public JsonResult GetAllUsers()
        {
            return Json(UserUtils.GetAllUsers(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllRoles()
        {
            return Json(UserUtils.GetAllRoles(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUserDetail(string username)
        {
            return Json(UserUtils.GetUserDetail(username), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ChangePassword(string userName, string pass, string repeatPass)
        {
            var model = new RegisterViewModel
            {
                UserName = userName,
                Password = pass,
                ConfirmPassword = repeatPass
            };

            return UserUtils.ChangePassword(model);
        }
        [HttpGet]
        public string DeleteUser(string username)
        {
            return username == "admin" ? "Невозможно удалить данную учетную запись" :  UserUtils.DeleteUser(username);
        }
        /*[HttpPost]
        public string UpdateUser(UserSaveEdit saveedit)
        {
            return UserUtils.EditUser(saveedit);
        } */
        [HttpPost]
        public string AddUser(string userName, string pass, string repeatPass)
        {
            var model = new RegisterViewModel
            {
                UserName = userName,
                Password = pass,
                ConfirmPassword = repeatPass
            };

            return UserUtils.AddUser(model, "", null);
        }
    }
}