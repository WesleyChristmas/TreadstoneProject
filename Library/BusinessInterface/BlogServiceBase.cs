using System.Collections.Generic;
using BusinessEntity;
using BusinessEntity.Models;

namespace BusinessInterface
{
    public interface IBlogServiceBase
    {
        List<BlogEntityLight> GetBlogs();
        BlogEntity GetBlogDetails(int idBlog);
        void AddBlog(BlogEntity blog, List<ReceiveFileModel> images);
        void EditBlog(BlogEntity blog);
        void AddPhoto2BLog(List<ReceiveFileModel> images, int idBlog);
        void RemovePhotoFromBLog(List<int> idRecord, string serverPath);
        void DeleteBlog(int idBLog, string serverPath);
    }
}
