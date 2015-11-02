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

        public List<> 
    }
}
