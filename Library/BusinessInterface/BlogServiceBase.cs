using System.Collections.Generic;
using BusinessEntity;
using BusinessEntity.Models;

namespace BusinessInterface
{
    public interface IBlogServiceBase
    {
        List<BlogEntity> GetBlogsPage(int page);
        void AddBlog(BlogEntity blog, List<ReceiveFileModel> images);
        void EditBlog(BlogEntity blog);
        void AddPhoto2BLog(List<ReceiveFileModel> images, int idBlog);
        void RemovePhotoFromBLog(List<int> idRecord);
        void DeleteBlog(int idBLog);
    }
}
