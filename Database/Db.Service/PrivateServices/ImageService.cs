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
        void DeleteImage(int idPhoto);
        string GetWebLink();
    }
    public class ImageService : Service<Photo>, IImageService
    {
        private readonly IRepositoryAsync<Photo> _photoRepository;
        private readonly IRepositoryAsync<PhotoType> _photoTypeRepository;
        private readonly IRepositoryAsync<SiteSetting> _setting; 
        private readonly IUnitOfWorkAsync _uof;

        public ImageService(IRepositoryAsync<Photo> photoRepository,
            IRepositoryAsync<PhotoType> photoTypeRepository,
            IRepositoryAsync<SiteSetting> setting,
            IUnitOfWorkAsync uof) : base(photoRepository)
        {
            _photoRepository = photoRepository;
            _photoTypeRepository = photoTypeRepository;
            _setting = setting;
            _uof = uof;
        }

        public int SaveImage(ReceiveFileModel image)
        {
            var imgtype = _photoTypeRepository.Queryable().FirstOrDefault(x => x.IdRecord == image.IdSection);
            if (imgtype == null) return -1;

            var ways2Img = _setting.Queryable().FirstOrDefault();
            if (ways2Img == null) return -1;

            var curDir = ways2Img.ImageDataDrive + "\\";
            if (!Directory.Exists(curDir + imgtype.Name)) Directory.CreateDirectory(curDir + imgtype.Name);
            File.WriteAllBytes(ways2Img.ImageDataDrive + "/" + imgtype.Name + "/" + image.FileName, image.Data);

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

            var ways2Img = _setting.Queryable().FirstOrDefault();
            if (ways2Img == null) return;

            var imgtype = _photoTypeRepository.Queryable().FirstOrDefault(x => x.IdRecord == image.IdSection);
            if (imgtype == null) return;

            File.Delete(ways2Img.ImageDataDrive + photo.Link);

            File.WriteAllBytes(ways2Img.ImageDataDrive + "/" + imgtype.Name + "/" + image.FileName, image.Data);
            photo.Link = "/" + imgtype.Name + "/" + image.FileName;
            _photoRepository.Update(photo);
            _uof.SaveChanges();
        }

        public void DeleteImage(int idPhoto)
        {
            var photo = _photoRepository.Queryable().FirstOrDefault(x => x.IdRecord == idPhoto);
            if (photo == null) return;

            var ways2Img = _setting.Queryable().FirstOrDefault();
            if (ways2Img == null) return;

            var driveWay = ways2Img.ImageDataDrive + photo.Link;
            File.Delete(driveWay);
            _photoRepository.Delete(idPhoto);
        }

        public string GetWebLink()
        {
            var setting = _setting.Queryable().FirstOrDefault();
            return setting == null ? null : setting.ImageDataWeb;
        }
    }
}
