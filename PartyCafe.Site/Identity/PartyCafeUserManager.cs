using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PartyCafe.Site.Identity
{
    public class PartyCafeUserManager : UserManager<PartyCafeUser>
    {
        public PartyCafeUserManager(PartyCafeUserStore store) : base(store)
        {
            this.PasswordHasher = new PartyCafePasswordHasher();
        }

        public override Task<PartyCafeUser> FindAsync(string userName, string password)
        {
            Task<PartyCafeUser> taskInvoke = Task<PartyCafeUser>.Factory.StartNew(() =>
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