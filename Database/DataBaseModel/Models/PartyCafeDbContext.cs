using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataBase.Models.Mapping;

namespace DataBase.Models
{
    public partial class PartyCafeDbContext : DbContext
    {
        static PartyCafeDbContext()
        {
            Database.SetInitializer<PartyCafeDbContext>(null);
        }

        public PartyCafeDbContext()
            : base("Name=PartyCafeDbContext")
        {
        }

        public DbSet<FoodMenu> FoodMenus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FoodMenuMap());
        }
    }
}
