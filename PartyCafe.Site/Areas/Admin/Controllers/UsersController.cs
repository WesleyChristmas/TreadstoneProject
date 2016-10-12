using System.Collections.Generic;
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
        public ActionResult UsersHome()
        {
            return View("UsersHome");
        }
        [HttpGet]
        public ActionResult UsersAdd()
        {
            return View("UsersAdd");
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

        [HttpPost]
        public JsonResult GetUserDetail(string username)
        {
            return Json(UserUtils.GetUserDetail(username), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string ChangePassword(RegisterViewModel model)
        {
            return UserUtils.ChangePassword(model);
        }
        [HttpPost]
        public string DeleteUser(string username)
        {
            return UserUtils.DeleteUser(username);
        }
        /*[HttpPost]
        public string UpdateUser(UserSaveEdit saveedit)
        {
            return UserUtils.EditUser(saveedit);
        } */
        [HttpPost]
        public string AddUser(RegisterViewModel model, string description, List<string> roles)
        {
            if (ModelState.IsValid)
            {
                return UserUtils.AddUser(model, description, roles); 
            }
            else
            {
                return "bad";
            }
        }
    }
}