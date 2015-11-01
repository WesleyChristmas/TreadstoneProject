using Repository.Pattern.Ef6;

namespace Entity.Model
{
    public class SiteSetting : Entity<int>
    {
        public string ImageDataWeb { get; set; }
        public string ImageDataDrive { get; set; }
    }
}
