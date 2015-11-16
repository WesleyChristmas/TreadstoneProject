using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessEntity;
using BusinessEntity.Models;
using BusinessInterface;
using Db.Service.PrivateServices;
using Entity.Model;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace Db.Service
{
    public interface IFoodMenuService : IService<FoodMenu>, IFoodMenuServiceBase
    {

    }

    public class FoodMenuService : Service<FoodMenu>, IFoodMenuService
    {
        private readonly IImageService _imageService;
        private readonly IRepositoryAsync<FoodMenu> _repositoryMenu;
        private readonly IRepositoryAsync<FoodMenuType> _repositoryMenuType;
        private readonly IUnitOfWorkAsync _unitOfWork;

        private readonly string _weblink;

        public FoodMenuService(IImageService imageService,
            IRepositoryAsync<FoodMenu> repositoryMenu,
            IRepositoryAsync<FoodMenuType> repositoryMenuType,
            IUnitOfWorkAsync unitOfWork)
            : base(repositoryMenu)
        {
            _imageService = imageService;
            _repositoryMenu = repositoryMenu;
            _repositoryMenuType = repositoryMenuType;
            _unitOfWork = unitOfWork;

            _weblink = _imageService.GetWebLink();

            #region Mapper

            Mapper.CreateMap<FoodMenu, FoodMenuEntity>();
            Mapper.CreateMap<FoodMenuEntity, FoodMenu>();

            Mapper.CreateMap<FoodMenuType, FoodMenuTypeEntity>()
                .ForMember(x => x.PhotoLink, opt => opt.MapFrom(r => _weblink + r.Photo.Link));
            Mapper.CreateMap<FoodMenuTypeEntity, FoodMenuType>();
            Mapper.CreateMap<FoodMenu, FoodMenuEntity>()
                .ForMember(x => x.PhotoLink, opt => opt.MapFrom(r => _weblink + r.Photo.Link));
            Mapper.CreateMap<FoodMenuEntity, FoodMenu>();

            #endregion
        }

        #region FoodmenuTypes

        public List<FoodMenuTypeEntity> GetAllFoodMenuTypes()
        {
            return Mapper.Map<List<FoodMenuType>, List<FoodMenuTypeEntity>>(_repositoryMenuType.Query()
                .Include(x => x.Photo)
                .Select()
                .ToList());
        }

        public void AddFoodMenuType(FoodMenuTypeEntity type, ReceiveFileModel image)
        {
            var dbtype = Mapper.Map<FoodMenuTypeEntity, FoodMenuType>(type);
            if (image != null) dbtype.IdPhoto = _imageService.SaveImage(image);
            _repositoryMenuType.Insert(dbtype);
            _unitOfWork.SaveChanges();
        }

        public void UpdateMenuType(FoodMenuTypeEntity type, ReceiveFileModel image)
        {
           var updateType = _repositoryMenuType.Queryable().FirstOrDefault(x => x.IdRecord == type.IdRecord);
            if (updateType == null) return;

            if (image != null)
            {
                if (updateType.IdPhoto.HasValue) _imageService.UpdateImage(image, updateType.IdPhoto.Value);
                else updateType.IdPhoto = _imageService.SaveImage(image);
            }

            updateType.Name = type.Name;
            updateType.Description = type.Description;

            _repositoryMenuType.Update(updateType);
            _unitOfWork.SaveChanges();
        }

        public bool DeleteMenuType(int idType)
        {
            var type = _repositoryMenuType.Queryable().FirstOrDefault(x => x.IdRecord == idType);
            if (type == null) return false;

            if (_repositoryMenu.Queryable().Count(x => x.IdType == idType) != 0) return false;

            if (type.IdPhoto.HasValue) _imageService.DeleteImage(type.IdPhoto.Value);

            _repositoryMenuType.Delete(idType);
            _unitOfWork.SaveChanges();
            return true;
        }

        #endregion

        #region FoodMenu

        public List<FoodMenuEntity> GetFoodMenu(int idType)
        {
            var menu = _repositoryMenu.Query()
                .Include(x => x.Photo)
                .Select()
                .Where(x => x.IdType == idType)
                .ToList();


           return Mapper.Map<List<FoodMenu>, List<FoodMenuEntity>>(menu);
        }

        public void AddFoodMenu(FoodMenuEntity menuItem, ReceiveFileModel image)
        {
            _repositoryMenu.Insert(Mapper.Map<FoodMenuEntity,FoodMenu>(menuItem));
            _unitOfWork.SaveChanges();
        }

        public void UpdateFoodMenu(FoodMenuEntity menuItem,ReceiveFileModel image)
        {
            var dbfoodMenu = _repositoryMenu.Queryable().FirstOrDefault(x => x.IdRecord == menuItem.IdRecord);
            if (dbfoodMenu == null) return;
            if (image != null) dbfoodMenu.IdPhoto = _imageService.SaveImage(image);
            dbfoodMenu.Name = menuItem.Name;
            dbfoodMenu.Description = menuItem.Description;
            dbfoodMenu.Price = menuItem.Price;
            _repositoryMenu.Update(dbfoodMenu);
            _unitOfWork.SaveChanges();
        }

        public bool DeleteFoodMenu(int idMenuItem)
        {
            var menuItem = _repositoryMenuType.Queryable().FirstOrDefault(x => x.IdRecord == idMenuItem);
            if (menuItem == null) return false;

            if (menuItem.IdPhoto.HasValue) _imageService.DeleteImage(menuItem.IdPhoto.Value);

            _repositoryMenuType.Delete(idMenuItem);
            _unitOfWork.SaveChanges();
            return true;
        }

        #endregion
    }
}
