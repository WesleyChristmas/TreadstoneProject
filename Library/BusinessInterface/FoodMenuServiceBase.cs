using System.Collections.Generic;
using BusinessEntity;
using BusinessEntity.Models;

namespace BusinessInterface
{
    public interface IFoodMenuServiceBase
    {
        List<FoodMenuTypeEntity> GetAllFoodMenuTypes();
        void AddFoodMenuType(FoodMenuTypeEntity type, ReceiveFileModel image);
        void UpdateMenuType(FoodMenuTypeEntity type, ReceiveFileModel image);
        bool DeleteMenuType(int idType);
    }
}
