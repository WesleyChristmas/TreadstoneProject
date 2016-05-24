using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PartyCafe.Site.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(ApplicationUserStore store) : base(store)
        {
            this.PasswordHasher = new ApplicationPasswordHasher();
        }

        public override Task<ApplicationUser> FindAsync(string userName, string password)
        {
            Task<ApplicationUser> taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                var user = Store.FindByNameAsync(userName).Result;
                PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(user.Password, password);
                if (result == PasswordVerificationResult.Success)
                {
                    return user;
                }
                return null;
            });
            return taskInvoke;
        }
    }
}