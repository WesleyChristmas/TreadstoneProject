using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class PhotoType
    {
        public PhotoType()
        {
            this.Photos = new List<Photo>();
        }

        public int IdRecord { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
