using System;
using Microsoft.AspNet.Identity;

namespace partycafev2.Models
{
    public class User : IUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime? TimeLimit { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        string IUser<string>.Id
        {
            get { return UserId.ToString(); }
        }
    }

    public class Role : IRole
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }

        string IRole<string>.Id
        {
            get { return RoleId.ToString(); }
        }
    }
}