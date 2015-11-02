using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public sealed class Photo : AuditableEntity<int>
    {
        public Photo()
        {
            BlogCalendars = new List<BlogCalendar>();
            FoodMenuTypes = new List<FoodMenuType>();
        }

        public int IdType { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public ICollection<BlogCalendar> BlogCalendars { get; set; }
        public ICollection<FoodMenuType> FoodMenuTypes { get; set; }
        public ICollection<BlogPhoto> BlogPhotos { get; set; }
        public PhotoType PhotoType { get; set; }
    }
}
