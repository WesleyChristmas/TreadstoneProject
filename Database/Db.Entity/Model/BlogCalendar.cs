using System;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class BlogCalendar : Entity<int>
    {
        public int? IdBlog { get; set; }
        public int? IdPhoto { get; set; }
        public string Header { get; set; }
        public DateTime EventDate { get; set; }

        public virtual BlogHeader BlogHeader { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
