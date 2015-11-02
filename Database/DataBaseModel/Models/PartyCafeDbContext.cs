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

        public DbSet<BlogCalendar> BlogCalendars { get; set; }
        public DbSet<BlogHeader> BlogHeaders { get; set; }
        public DbSet<BlogPhoto> BlogPhotoes { get; set; }
        public DbSet<FoodMenu> FoodMenus { get; set; }
        public DbSet<FoodMenuType> FoodMenuTypes { get; set; }
        public DbSet<GameBody> GameBodies { get; set; }
        public DbSet<GameHeader> GameHeaders { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoType> PhotoTypes { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BlogCalendarMap());
            modelBuilder.Configurations.Add(new BlogHeaderMap());
            modelBuilder.Configurations.Add(new BlogPhotoMap());
            modelBuilder.Configurations.Add(new FoodMenuMap());
            modelBuilder.Configurations.Add(new FoodMenuTypeMap());
            modelBuilder.Configurations.Add(new GameBodyMap());
            modelBuilder.Configurations.Add(new GameHeaderMap());
            modelBuilder.Configurations.Add(new PhotoMap());
            modelBuilder.Configurations.Add(new PhotoTypeMap());
            modelBuilder.Configurations.Add(new SiteSettingMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
