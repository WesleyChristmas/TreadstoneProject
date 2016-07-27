using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace PartyCafe.Site.Models
{
    public class User : IUser<string>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime? TimeLimit { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        string IUser<string>.Id => UserId.ToString();
    }

    public class Role : IRole<string>
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        string IRole<string>.Id => RoleId.ToString();
    }
}