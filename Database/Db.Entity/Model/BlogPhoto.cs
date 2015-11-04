using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class BlogPhoto : Entity<int>
    {
        public int IdHeader { get; set; }
        public int IdPhoto { get; set; }
        public string Description { get; set; }

        public BlogHeader BlogHeader { get; set; }
        public Photo Photo { get; set; }
    }
}
