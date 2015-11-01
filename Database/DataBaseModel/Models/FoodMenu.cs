using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class FoodMenu
    {
        public int IdRecord { get; set; }
        public int IdType { get; set; }
        public Nullable<int> IdPhoto { get; set; }
        public string Name { get; set; }
        public Nullable<int> Description { get; set; }
        public Nullable<double> Price { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual FoodMenuType FoodMenuType { get; set; }
    }
}
