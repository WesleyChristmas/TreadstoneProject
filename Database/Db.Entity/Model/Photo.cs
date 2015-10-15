using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public sealed class Photo : AuditableEntity<int>
    {
        public Photo()
        {
            BlogBodies = new List<BlogBody>();
            BlogCalendars = new List<BlogCalendar>();
            FoodMenus = new List<FoodMenu>();
            FoodMenuTypes = new List<FoodMenuType>();
            GameBodies = new List<GameBody>();
            GameHeaders = new List<GameHeader>();
        }

        public int IdType { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public ICollection<BlogBody> BlogBodies { get; set; }
        public ICollection<BlogCalendar> BlogCalendars { get; set; }
        public ICollection<FoodMenu> FoodMenus { get; set; }
        public ICollection<FoodMenuType> FoodMenuTypes { get; set; }
        public ICollection<GameBody> GameBodies { get; set; }
        public ICollection<GameHeader> GameHeaders { get; set; }
        public PhotoType PhotoType { get; set; }
    }
}
