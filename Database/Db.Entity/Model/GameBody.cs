using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class GameBody : AuditableEntity<int>
    {
        public int? IdPhoto { get; set; }
        public int IdHeader { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public virtual GameHeader GameHeader { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
