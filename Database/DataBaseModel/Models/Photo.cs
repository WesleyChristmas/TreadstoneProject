using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Photo
    {
        public Photo()
        {
            this.BlogCalendars = new List<BlogCalendar>();
            this.BlogPhotoes = new List<BlogPhoto>();
            this.FoodMenus = new List<FoodMenu>();
            this.FoodMenuTypes = new List<FoodMenuType>();
        }

        public int IdRecord { get; set; }
        public int IdType { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual ICollection<BlogCalendar> BlogCalendars { get; set; }
        public virtual ICollection<BlogPhoto> BlogPhotoes { get; set; }
        public virtual ICollection<FoodMenu> FoodMenus { get; set; }
        public virtual ICollection<FoodMenuType> FoodMenuTypes { get; set; }
    }
}
