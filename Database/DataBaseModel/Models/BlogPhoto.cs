using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class BlogPhoto
    {
        public int IdRecord { get; set; }
        public int IdHeader { get; set; }
        public int IdPhoto { get; set; }
        public string Description { get; set; }
    }
}
