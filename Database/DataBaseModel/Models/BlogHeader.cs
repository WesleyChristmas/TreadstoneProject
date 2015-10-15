using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class BlogHeader
    {
        public BlogHeader()
        {
            this.BlogBodies = new List<BlogBody>();
            this.BlogCalendars = new List<BlogCalendar>();
        }

        public int IdRecord { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual ICollection<BlogBody> BlogBodies { get; set; }
        public virtual ICollection<BlogCalendar> BlogCalendars { get; set; }
    }
}
