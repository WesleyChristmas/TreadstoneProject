using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Identity
{
    public class ApplicationUserStore : IUserStore<ApplicationUser>
    {
        public Task CreateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => {
                var db = MainUtils.GetDBContext();
                Users newUser = new Users();
                newUser.Login = user.UserName;
                newUser.Password = user.Password;
                db.Users.InsertOnSubmit(newUser);
                db.SubmitChanges();
            });
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return Task.Factory.StartNew(() =>
            {
                var db = MainUtils.GetDBContext();
                var currUser = (from u in db.Users
                                where u.IdentityId == new Guid(userId)
                                select u).SingleOrDefault();

                ApplicationUser user = new ApplicationUser(currUser.IdentityId, currUser.Login, currUser.Password);
                return user;
            });
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() => {
                var db = MainUtils.GetDBContext();
                var currUser = (from u in db.Users
                                where u.Login == userName
                                select u).SingleOrDefault();

                return new ApplicationUser(currUser.IdentityId, currUser.Login, currUser.Password);
            });
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}