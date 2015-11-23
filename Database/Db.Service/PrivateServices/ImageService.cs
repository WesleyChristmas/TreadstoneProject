using System.IO;
using System.Linq;
using BusinessEntity.Models;
using Entity.Model;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace Db.Service.PrivateServices
{
    public interface IImageService : IService<Photo>
    {
        int SaveImage(ReceiveFileModel image);
        void UpdateImage(ReceiveFileModel image, int idPhoto);
        void DeleteImage(int idPhoto,string serverPath);
    }
    public class ImageService : Service<Photo>, IImageService
    {
        private readonly IRepositoryAsync<Photo> _photoRepository;
        private readonly IRepositoryAsync<PhotoType> _photoTypeRepository;
        private readonly IUnitOfWorkAsync _uof;

        public ImageService(IRepositoryAsync<Photo> photoRepository,
            IRepositoryAsync<PhotoType> photoTypeRepository,
            IUnitOfWorkAsync uof) : base(photoRepository)
        {
            _photoRepository = photoRepository;
            _photoTypeRepository = photoTypeRepository;
            _uof = uof;
        }

        public int SaveImage(ReceiveFileModel image)
        {
            var imgtype = _photoTypeRepository.Queryable().FirstOrDefault(x => x.IdRecord == image.IdSection);
            if (imgtype == null) return -1;

            var curDir = image.ServerPath + "/";
            if (!Directory.Exists(curDir + imgtype.Name)) Directory.CreateDirectory(curDir + imgtype.Name);
            File.WriteAllBytes(image.ServerPath + "/" + imgtype.Name + "/" + image.FileName, image.Data);

            var photo = new Photo
            {
                IdType = imgtype.IdRecord,
                Description = "sysimg",
                Link = "/" + imgtype.Name + "/" + image.FileName
            };

            _photoRepository.Insert(photo);
            _uof.SaveChanges();
            return photo.IdRecord;
        }

        public void UpdateImage(ReceiveFileModel image, int idPhoto)
        {
            var photo = _photoRepository.Query()
                .Include(x=>x.PhotoType)
                .Select()
                .FirstOrDefault(x => x.IdRecord == idPhoto);
            if (photo == null) return;

            var imgtype = _photoTypeRepository.Queryable().FirstOrDefault(x => x.IdRecord == image.IdSection);
            if (imgtype == null) return;

            File.Delete(image.ServerPath + photo.Link);

            File.WriteAllBytes(image.ServerPath + "/" + imgtype.Name + "/" + image.FileName, image.Data);
            photo.Link = "/" + imgtype.Name + "/" + image.FileName;
            _photoRepository.Update(photo);
            _uof.SaveChanges();
        }

        public void DeleteImage(int idPhoto,string serverPath)
        {
            var photo = _photoRepository.Queryable().FirstOrDefault(x => x.IdRecord == idPhoto);
            if (photo == null) return;

            var driveWay = serverPath + photo.Link;
            File.Delete(driveWay);
            _photoRepository.Delete(idPhoto);
        }
    }
}
