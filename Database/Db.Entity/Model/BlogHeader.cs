using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public sealed class BlogHeader : AuditableEntity<int>
    {
        public BlogHeader()
        {
            BlogBodies = new List<BlogBody>();
            BlogCalendars = new List<BlogCalendar>();
        }

        public string Message { get; set; }
        public DateTime? EventDate { get; set; }
        public ICollection<BlogBody> BlogBodies { get; set; }
        public ICollection<BlogCalendar> BlogCalendars { get; set; }
    }
}
