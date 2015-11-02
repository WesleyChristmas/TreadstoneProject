using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class BlogHeader
    {
        public BlogHeader()
        {
            this.BlogCalendars = new List<BlogCalendar>();
            this.BlogPhotoes = new List<BlogPhoto>();
        }

        public int IdRecord { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string UserCreate { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public virtual ICollection<BlogCalendar> BlogCalendars { get; set; }
        public virtual ICollection<BlogPhoto> BlogPhotoes { get; set; }
    }
}
