using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class BlogCalendar
    {
        public int IdRecord { get; set; }
        public Nullable<int> IdBlog { get; set; }
        public Nullable<int> IdPhoto { get; set; }
        public string Header { get; set; }
        public System.DateTime EventDate { get; set; }
        public virtual BlogHeader BlogHeader { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
