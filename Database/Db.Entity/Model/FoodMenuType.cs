using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public sealed class FoodMenuType : AuditableEntity<int>
    {
        public FoodMenuType()
        {
            FoodMenus = new List<FoodMenu>();
        }

        public int? IdPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<FoodMenu> FoodMenus { get; set; }
        public Photo Photo { get; set; }
    }
}
