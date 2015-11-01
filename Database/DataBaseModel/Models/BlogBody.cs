using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class BlogBody
    {
        public int IdRecord { get; set; }
        public int IdHeader { get; set; }
        public Nullable<int> IdPhoto { get; set; }
        public string Message { get; set; }
        public virtual BlogHeader BlogHeader { get; set; }
    }
}
