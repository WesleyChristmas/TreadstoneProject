using System.Collections.Generic;
using System.Linq;
using BusinessEntity;
using BusinessEntity.Models;
using Db.Service.PrivateServices;
using Entity.Model;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace Db.Service
{
    public interface IBlogService : IService<BlogHeader>
    {
        
    }
    public class BlogService : Service<BlogHeader>, IBlogService
    {
        private readonly IImageService _imageService;
        private readonly IRepositoryAsync<BlogHeader> _repositoryHeader;
        private readonly IRepositoryAsync<BlogPhoto> _repositoryPhoto;
        private readonly IUnitOfWorkAsync _uof;

        private const int PageSize = 10;

        public BlogService(IImageService imageService,
            IRepositoryAsync<BlogPhoto> repositoryPhoto
            , IRepositoryAsync<BlogHeader> repositoryHeader,
            IUnitOfWorkAsync uof) : base(repositoryHeader)
        {
            _imageService = imageService;
            _repositoryHeader = repositoryHeader;
            _repositoryPhoto = repositoryPhoto;
            _uof = uof;
        }

        public List<BlogEntity> GetBlogsPage(int page)
        {
            var result = new List<BlogEntity>();
           _repositoryHeader.Query()
                .Include(x => x.BlogPhotos.Select(r => r.Photo))
                .Select()
                .Skip(page*PageSize)
                .Take(PageSize)
                .ToList()
                .ForEach(x =>
                {
                    var blog = new BlogEntity
                    {
                        IdRecord = x.IdRecord,
                        Header = x.Header,
                        Message = x.Message,
                    };
                    var photos = new List<BlogPhotoEntity>();
                    x.BlogPhotos.ToList()
                        .ForEach(r =>
                        {
                            photos.Add(new BlogPhotoEntity
                            {
                                IdRecord = r.IdRecord,
                                Description = r.Description,
                               // PhotoLink = _weblink + r.Photo.Link
                            });
                        });
                    blog.Photos = photos;
                });

            return result;
        }

        public void AddBlog(BlogEntity blog, List<ReceiveFileModel> images)
        {
            var blogDb = new BlogHeader
            {
                Header = blog.Header,
                Message = blog.Message
            };

            _repositoryHeader.Insert(blogDb);
            _uof.SaveChanges();

            AddPhoto2BLog(images,blogDb.IdRecord);
        }

        public void EditBlog(BlogEntity blog)
        {
           var updateBlog =  _repositoryHeader.Queryable().FirstOrDefault(x => x.IdRecord == blog.IdRecord);
            if (updateBlog == null) return;
            updateBlog.Header = blog.Header;
            updateBlog.Message = blog.Message;
            _repositoryHeader.Update(updateBlog);
            _uof.SaveChanges();
        }

        public void AddPhoto2BLog(List<ReceiveFileModel> images, int idBlog)
        {
            var blog = _repositoryHeader.Queryable().FirstOrDefault(x => x.IdRecord == idBlog);
            if (blog == null) return;

            if (images != null && images.Count > 0)
            {
                images.ForEach(x =>
                {
                    _repositoryPhoto.Insert(new BlogPhoto
                    {
                        IdHeader = blog.IdRecord,
                        IdPhoto = _imageService.SaveImage(x)
                    });
                });
                _uof.SaveChangesAsync();
            }
        }

        public void RemovePhotoFromBLog(List<int> idRecord,string serverPath)
        {
            var photo2Del = _repositoryPhoto.Queryable().Where(x => idRecord.Contains(x.IdRecord))
                .ToList();
            if (photo2Del.Count < 1) return;

            photo2Del.ForEach(x =>
            {
                _imageService.DeleteImage(x.IdPhoto,serverPath);
                _repositoryPhoto.Delete(x.IdRecord);
            });
        }

        public void DeleteBlog(int idBLog,string serverPath)
        {
           var delBLog =  _repositoryHeader.Queryable().FirstOrDefault(x => x.IdRecord == idBLog);
            if (delBLog == null) return;
            var delBlogPhotoId = _repositoryPhoto.Queryable()
                .Where(x => x.IdHeader == delBLog.IdRecord)
                .ToList();
            if (delBlogPhotoId.Count > 0)
            {
                delBlogPhotoId.ForEach(x =>
                {
                    _imageService.DeleteImage(x.IdPhoto,serverPath);
                    _repositoryPhoto.Delete(x.IdRecord);
                });
            }

            _repositoryHeader.Delete(idBLog);
            _uof.SaveChanges();
        }
    }
}
