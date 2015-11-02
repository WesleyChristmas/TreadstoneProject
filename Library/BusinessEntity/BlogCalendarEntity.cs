using System;

namespace BusinessEntity
{
    public class BlogCalendarEntity
    {
        public int IdRecord { get; set; }
        public int? IdBlog { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Header { get; set; }
        public string PhotoLink { get; set; }
    }
}
