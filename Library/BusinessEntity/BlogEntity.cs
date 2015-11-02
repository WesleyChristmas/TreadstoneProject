using System.Collections.Generic;

namespace BusinessEntity
{
    public class BlogEntity
    {
        public int IdRecord { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public List<BlogPhotoEntity> Photos { get; set; }

        public BlogEntity()
        {
            Photos = new List<BlogPhotoEntity>();
        }
    }

    public class BlogPhotoEntity
    {
        public int IdRecord { get; set; }
        public string Description { get; set; }
        public string PhotoLink { get; set; }
    }

}
