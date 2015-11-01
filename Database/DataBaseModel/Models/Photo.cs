using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Photo
    {
        public Photo()
        {
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
        public virtual ICollection<FoodMenuType> FoodMenuTypes { get; set; }
    }
}
