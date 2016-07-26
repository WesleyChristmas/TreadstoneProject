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
    public class RoleStore : IRoleStore<Role>,
         IQueryableRoleStore<Role>
    {
        private readonly string _connectionString;

        public RoleStore(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        public RoleStore()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PartyCafeDbContext"].ConnectionString;
        }

        #region IRoleStore

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                    connection.Execute("update Roles set Name = @roleName, Description = @description where RoleId = @roleId",
                        new { @roleName = role.Name, @description = role.Description, @roleId = role.RoleId });
            });
        }

        public Task DeleteAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Role> FindByIdAsync(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
                throw new ArgumentNullException(nameof(roleId));

            Guid parsedRoleId;
            if (!Guid.TryParse(roleId, out parsedRoleId))
                throw new ArgumentOutOfRangeException(nameof(roleId), string.Format("'{0}' is not a valid GUID.", new { roleId }));

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                    return connection.Query<Role>("select * from Roles where RoleId = @roleId", new { roleId = parsedRoleId }).SingleOrDefault();
            });
        }

        public virtual Task<Role> FindByNameAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                    return connection.Query<Role>("select * from Roles where lower(Name) = lower(@roleName)", new { roleName }).SingleOrDefault();
            });
        }

        #endregion

        #region IQueryableRoleStore

        public IQueryable<Role> Roles
        {
            get
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var roles = connection.Query<Role>("select Name from Roles");
                    return roles.AsQueryable();
                }
            }
        }

        #endregion
    }
}