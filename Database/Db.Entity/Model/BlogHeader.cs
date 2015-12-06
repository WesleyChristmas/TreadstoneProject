using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class BlogHeader : AuditableEntity<int>
    {
        public BlogHeader()
        {
            BlogCalendars = new List<BlogCalendar>();
            BlogPhotos = new List<BlogPhoto>();
        }

        public string Header { get; set; }
        public string Message { get; set; }
        public ICollection<BlogCalendar> BlogCalendars { get; set; }
        public ICollection<BlogPhoto> BlogPhotos { get; set; }
    }
}
