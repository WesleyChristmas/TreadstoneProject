using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public sealed class GameHeader : AuditableEntity<int>
    {
        public GameHeader()
        {
            GameBodies = new List<GameBody>();
        }

        public int? IdPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<GameBody> GameBodies { get; set; }
        public Photo Photo { get; set; }
    }
}
