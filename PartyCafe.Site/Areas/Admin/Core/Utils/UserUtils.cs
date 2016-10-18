using System;
using System.Collections.Generic;
using System.Linq;
using PartyCafe.Site.Identity;
using PartyCafe.Site.Models;
using PartyCafe.Site.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace PartyCafe.Site.Areas.Admin.Core.Utils
{
    public class UserUtils
    {
        private static RoleManager<Role> _roleManager = new RoleManager<Role>(new RoleStore());
        private static UserManager<User> _userManager = new UserManager<User>(new UserStore());

        public static List<User> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }
        public static List<Role> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public static string ChangePassword(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword)) return "Поля не должны быть пустыми!";
            if (model.Password != model.ConfirmPassword) return "Пароли не совпадают!";

            IdentityResult error = null;
            try
            {
                var user = _userManager.FindByName(model.UserName);
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(model.Password);
                _userManager.Update(user);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



            return error.Succeeded ? "ok" : "bad";
        }

        public static UserDetail GetUserDetail(string username)
        {
            var result = new UserDetail();
            result.user = new List<User>();

            if (!string.IsNullOrWhiteSpace(username))
            {
                var user = _userManager.FindByName(username);
                result.user.Add(new User()
                {
                    PasswordHash = null,
                    SecurityStamp = null,
                    UserId = Guid.Empty,
                    UserName = user.UserName,
                    Description = user.Description
                });
                var role = _userManager.GetRoles(user.UserId.ToString());
                result.userroles = role.ToList();
            }

            return result;
        }

        public static string DeleteUser(string username)
        {
            var user = _userManager.FindByName(username);
            _userManager.Delete(user);

            return "ok";
        }
        public static string AddUser(RegisterViewModel model, string description, List<string> roles)
        {
            try
            {
                var user = new User()
                {
                    UserName = model.UserName,
                    Description = description
                };
                var result = _userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    if (roles == null) return "ok";
                    foreach (var item in roles)
                    {
                        _userManager.AddToRole(userId: user.UserId.ToString(), role: item);
                    }
                }
                else
                {
                    var response = "";
                    result.Errors.ForEach(x =>
                    {
                        response += x + "\n";
                    });
                    return response;
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

       /* public static string EditUser(UserSaveEdit saveedit)
        {
            var OldSearchedUser = _userManager.FindByName(saveedit.oldname);
            var userRoles = _userManager.GetRoles(userId: OldSearchedUser.UserId.ToString());

            OldSearchedUser.UserName = saveedit.oldname != saveedit.newname ? saveedit.oldname : saveedit.newname;
            OldSearchedUser.Description = saveedit.description;

            var deleteRoles = userRoles.Except(saveedit.roles).ToList();
            var createRoles = saveedit.roles.Except(userRoles).ToList();

            foreach (var item in deleteRoles)
            {
                _userManager.RemoveFromRole(userId: OldSearchedUser.UserId.ToString(), role: item);
            }

            foreach (var item in createRoles)
            {
                _userManager.AddToRole(userId: OldSearchedUser.UserId.ToString(), role: item);
            }

            _userManager.Update(OldSearchedUser);

            return "ok";
        }*/
    }
}