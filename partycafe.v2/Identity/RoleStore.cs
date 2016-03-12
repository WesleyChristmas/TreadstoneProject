using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using partycafev2.Models;
using Microsoft.AspNet.Identity;

namespace partycafev2.Identity
{
    public class RoleStore : IRoleStore<Role>,
        IQueryableRoleStore<Role>
    {
        private readonly string _connectionString;

        public RoleStore(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(connectionString);

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

        public Task UpdateAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
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