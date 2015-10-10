using System.Data.Entity;
using Entity.Mappings;
using Entity.Model;
using Repository.Pattern.Ef6;

namespace Entity
{
    public class PartyCafeDbContext : DataContext
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

