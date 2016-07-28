using System;
using System.Collections.Generic;
using PartyCafe.Site.Models;

namespace PartyCafe.Site.Areas.Admin.Models
{
    public class UserViewModel
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UserDetail
    {
        public List<User> user { get; set; }
        public List<Role> Roles { get; set; }
        public List<string> userroles { get; set; }
    }
    public class UserSaveEdit
    {
        public string oldname { get; set; }
        public string newname { get; set; }
        public string rolename { get; set; }
        public string description { get; set; }
        public List<string> roles { get; set; }
    }
}