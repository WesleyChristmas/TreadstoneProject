using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class GameBody
    {
        public int IdRecord { get; set; }
        public Nullable<int> IdPhoto { get; set; }
        public int IdHeader { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual GameHeader GameHeader { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
