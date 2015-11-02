using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public sealed class BlogHeader : AuditableEntity<int>
    {
        public BlogHeader()
        {
            BlogPhotos = new List<BlogPhoto>();
            BlogCalendars = new List<BlogCalendar>();
        }

        public string Header { get; set; }
        public string Message { get; set; }
        public DateTime? EventDate { get; set; }
        public ICollection<BlogCalendar> BlogCalendars { get; set; }
        public ICollection<BlogPhoto> BlogPhotos { get; set; }
    }
}
