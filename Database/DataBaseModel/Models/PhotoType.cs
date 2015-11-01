using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class PhotoType
    {
        public int IdRecord { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
    }
}
