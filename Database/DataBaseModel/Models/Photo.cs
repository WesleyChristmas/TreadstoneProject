using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Photo
    {
        public Photo()
        {
            this.BlogBodies = new List<BlogBody>();
            this.BlogCalendars = new List<BlogCalendar>();
            this.FoodMenus = new List<FoodMenu>();
            this.FoodMenuTypes = new List<FoodMenuType>();
            this.GameBodies = new List<GameBody>();
            this.GameHeaders = new List<GameHeader>();
        }

        public int IdRecord { get; set; }
        public int IdType { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual ICollection<BlogBody> BlogBodies { get; set; }
        public virtual ICollection<BlogCalendar> BlogCalendars { get; set; }
        public virtual ICollection<FoodMenu> FoodMenus { get; set; }
        public virtual ICollection<FoodMenuType> FoodMenuTypes { get; set; }
        public virtual ICollection<GameBody> GameBodies { get; set; }
        public virtual ICollection<GameHeader> GameHeaders { get; set; }
        public virtual PhotoType PhotoType { get; set; }
    }
}
