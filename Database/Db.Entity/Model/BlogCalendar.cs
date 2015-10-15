using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class BlogCalendar : AuditableEntity<int>
    {
        public int IdHeader { get; set; }
        public int? IdPhoto { get; set; }
        public string Header { get; set; }
        public virtual BlogHeader BlogHeader { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
