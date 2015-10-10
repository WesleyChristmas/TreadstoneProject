using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessEntity;
using Entity.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace Db.Service
{
    public interface IFoodMenuService : IService<FoodMenu>
    {
        List<FoodMenuEntity> GetAllMenu();
    }

    public class FoodMenuService : Service<FoodMenu>, IFoodMenuService
    {
        private readonly IRepositoryAsync<FoodMenu> _repositoryMenu;

        public FoodMenuService(IRepositoryAsync<FoodMenu> repositoryMenu)
            : base(repositoryMenu)
        {
            _repositoryMenu = repositoryMenu;

            Mapper.CreateMap<FoodMenu, FoodMenuEntity>();
            Mapper.CreateMap<FoodMenuEntity, FoodMenu>();
        }

        public List<FoodMenuEntity> GetAllMenu()
        {
            return Mapper.Map<List<FoodMenu>,List<FoodMenuEntity>>(_repositoryMenu.Queryable().ToList());
        }
    }
}
