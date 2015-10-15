using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class FoodMenuType
    {
        public FoodMenuType()
        {
            this.FoodMenus = new List<FoodMenu>();
        }

        public int IdRecord { get; set; }
        public Nullable<int> IdPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual ICollection<FoodMenu> FoodMenus { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
