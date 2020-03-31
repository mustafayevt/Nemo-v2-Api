using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> Get();  
        IEnumerable<Restaurant> GetBranches(long RestId);  
        Restaurant GetRestaurant(long id);  
        Restaurant GetParentByBranchId(long id);  
        Restaurant InsertRestaurant(Restaurant restaurant);  
        Restaurant UpdateRestaurant(Restaurant restaurant);  
        void DeleteRestaurant(long id); 
    }
}