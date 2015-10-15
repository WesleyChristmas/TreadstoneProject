using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public sealed class PhotoType : AuditableEntity<int>
    {
        public PhotoType()
        {
            Photos = new List<Photo>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
