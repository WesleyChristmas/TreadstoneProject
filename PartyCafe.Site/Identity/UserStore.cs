using Dapper;
using Microsoft.AspNet.Identity;
using PartyCafe.Site.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PartyCafe.Site.Identity
{
    public class UserStore : IUserStore<User>,
        IUserPasswordStore<User>,
        IUserSecurityStampStore<User>,
        IUserRoleStore<User>,
        IQueryableUserStore<User>
    {

        private readonly string _connectionString;

        public UserStore(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        public UserStore()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PartyCafeDbContext"].ConnectionString;
        }

        public void Dispose()
        {

        }

        #region IUserStore
        public virtual Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                user.UserId = Guid.NewGuid();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                    connection.Execute("insert into Users(UserId, UserName, Description, TimeLimit, PasswordHash, SecurityStamp)" +
                        " values(@userId, @userName, @description, @timeLimit, @passwordHash, @securityStamp)", user);
            });
        }

        public virtual Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                    connection.Execute("delete from UserRoles where UserId = @userId;" +
                                       "delete from Users where UserId = @userId", new { userId = user.UserId });
            });
        }
        public virtual Task<User> FindByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            Guid parsedUserId;
            if (!Guid.TryParse(userId, out parsedUserId))
                throw new ArgumentOutOfRangeException(nameof(userId), string.Format("'{0}' is not a valid GUID.", new { userId }));

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                    return connection.Query<User>("select * from Users where UserId = @userId", new { userId = parsedUserId }).SingleOrDefault();
            });
        }
        public virtual Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                    return connection.Query<User>("select * from Users where lower(UserName) = lower(@userName)", new { userName }).SingleOrDefault();
            });
        }
        public virtual Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                    connection.Execute("update Users set UserName = @userName, Description = @description, PasswordHash = @passwordHash, SecurityStamp = @securityStamp where UserId = @userId", user);
            });
        }
        #endregion

        #region IUserPasswordStore
        public virtual Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }
        public virtual Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
        public virtual Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }
        public virtual Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }
        #endregion

        #region IUSerRoleStore
        public virtual Task AddToRoleAsync(User user, string roleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("INSERT INTO UserRoles (UserId,RoleId)" +
                                   " VALUES (@userId,(SELECT TOP 1 RoleId FROM Roles WHERE Name=@roleName))",
                    new { userId = user.UserId, roleName });
                return Task.FromResult(0);
            }
        }
        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DELETE FROM UserRoles WHERE UserId = @userId AND" +
                                   " RoleId IN (SELECT TOP 1 RoleId From Roles WHERE Name = @roleName)",
                    new { userId = user.UserId, roleName });
                return Task.FromResult(0);
            }
        }
        public async Task<IList<string>> GetRolesAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var roles = await
                    connection.QueryAsync<Role>(
                        "SELECT RoleId, Name FROM dbo.Roles WHERE RoleId IN (SELECT RoleId FROM dbo.UserRoles WHERE UserId = @userID)",
                        new { userId = user.UserId });
                return roles.Select(x => x.Name).ToList();
            }
        }
        public IList<string> GetRoles(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var roles = connection.Query<Role>(
                        "SELECT RoleId, Name FROM dbo.Roles WHERE RoleId IN (SELECT RoleId FROM dbo.UserRoles WHERE UserId = @userID)",
                        new { userId = user.UserId });
                return roles.Select(x => x.Name).ToList();
            }
        }
        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var role = await
                    connection.QueryAsync<Role>(
                        "SELECT RoleId FROM SiteSpectDB.dbo.UserRoles WHERE UserId = @userId AND " +
                        "RoleId IN (SELECT TOP 1 RoleId From SiteSpectDB.dbo.Roles WHERE Name = @roleName)",
                        new { userId = user.UserId, roleName });

                return role != null;
            }
        }
        #endregion

        #region IQueryableUserStore
        public IQueryable<User> Users
        {
            get
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var users = connection.Query<User>("select UserName, Description from Users");
                    return users.AsQueryable();
                }
            }
        }
        #endregion
    }
}