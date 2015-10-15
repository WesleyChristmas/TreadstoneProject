using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class FoodMenu : AuditableEntity<int>
    {
        public int IdType { get; set; }
        public int? IdPhoto { get; set; }
        public string Name { get; set; }
        public int? Description { get; set; }
        public double? Price { get; set; }
        public virtual FoodMenuType FoodMenuType { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
