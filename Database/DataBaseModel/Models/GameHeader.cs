using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class GameHeader
    {
        public GameHeader()
        {
            this.GameBodies = new List<GameBody>();
        }

        public int IdRecord { get; set; }
        public Nullable<int> IdPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual ICollection<GameBody> GameBodies { get; set; }
    }
}
