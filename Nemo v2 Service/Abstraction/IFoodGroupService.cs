using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IFoodGroupService
    {
        IEnumerable<FoodGroup> Get();  
        IEnumerable<FoodGroup> GetFoodGroupByRestaurantId(long RestId);  
        FoodGroup GetFoodGroup(long id);  
        FoodGroup InsertFoodGroup(FoodGroup FoodGroup);  
        FoodGroup UpdateFoodGroup(FoodGroup FoodGroup);  
        void DeleteFoodGroup(long id); 
    }
}