using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class FoodMenu : AuditableEntity<int>
    {
        public int IdType { get; set; }
        public int? IdPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public FoodMenuType FoodMenuType { get; set; }
        public Photo Photo { get; set; }
    }
}
