using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class FoodMenu
    {
        public int IdRecord { get; set; }
        public int IdType { get; set; }
        public string IdPhoto { get; set; }
        public int Name { get; set; }
        public int Description { get; set; }
        public double Price { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
    }
}
