using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IFoodService
    {
        IEnumerable<Food> Get();  
        IEnumerable<Food> GetFoodByRestaurantId(long RestId);  
        Food GetFood(long id);  
        Food InsertFood(Food Food);  
        Food UpdateFood(Food Food);  
        void DeleteFood(long id);
    }
}