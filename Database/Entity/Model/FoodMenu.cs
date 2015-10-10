using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class FoodMenu : AuditableEntity<int>
    {
        public int IdType { get; set; }
        public string IdPhoto { get; set; }
        public int Name { get; set; }
        public int Description { get; set; }
        public double Price { get; set; }
    }
}
