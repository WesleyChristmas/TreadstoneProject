using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessEntity;
using BusinessInterface;
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
        private readonly IRepositoryAsync<FoodMenu> _repositoryMenu;
        private readonly IRepositoryAsync<FoodMenuType> _repositoryMenuType;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public FoodMenuService(IRepositoryAsync<FoodMenu> repositoryMenu,
            IRepositoryAsync<FoodMenuType> repositoryMenuType,
            IUnitOfWorkAsync unitOfWork)
            : base(repositoryMenu)
        {
            _repositoryMenu = repositoryMenu;
            _repositoryMenuType = repositoryMenuType;
            _unitOfWork = unitOfWork;

            Mapper.CreateMap<FoodMenu, FoodMenuEntity>();
            Mapper.CreateMap<FoodMenuEntity, FoodMenu>();

            Mapper.CreateMap<FoodMenuType, FoodMenuTypeEntity>()
                .ForMember(x => x.PhotoLink, opt => opt.MapFrom(r => r.Photo.Link));
            Mapper.CreateMap<FoodMenuTypeEntity, FoodMenuType>();
        }

        public List<FoodMenuTypeEntity> GetAllFoodMenuTypes()
        {
            return Mapper.Map<List<FoodMenuType>, List<FoodMenuTypeEntity>>(_repositoryMenuType.Query()
                .Include(x => x.Photo)
                .Select()
                .ToList());
        }

        public void AddFoodMenuType(FoodMenuTypeEntity type)
        {
            _repositoryMenuType.Insert(Mapper.Map<FoodMenuTypeEntity,FoodMenuType>(type));
            _unitOfWork.SaveChanges();
        }

        public void UpdateMenuType(FoodMenuTypeEntity type)
        {
           var updateType = _repositoryMenuType.Queryable().FirstOrDefault(x => x.IdRecord == type.IdRecord);
            if (updateType == null) return;

            updateType.Name = type.Name;
            updateType.Description = type.Description;

            _repositoryMenuType.Update(updateType);
            _unitOfWork.SaveChanges();
        }

        public bool DeleteMenuType(int idType)
        {
            if (_repositoryMenu.Queryable().Count(x => x.IdType == idType) != 0) return false;
            _repositoryMenuType.Delete(idType);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
