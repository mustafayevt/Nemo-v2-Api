using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class RestaurantService:IRestaurantService
    {
        private IRepository<Restaurant> _restaurantRepository;  
  
        public RestaurantService(IRepository<Restaurant> restaurantRepository)  
        {  
            this._restaurantRepository = restaurantRepository;
        }  
        public IEnumerable<Restaurant> Get()
        {
            return _restaurantRepository.Get();
        }

        public IEnumerable<Restaurant> GetBranches(long RestId)
        {
            return _restaurantRepository.Query(x => x.BranchId == RestId);
        }

        public Restaurant GetRestaurant(long id)
        {
            return _restaurantRepository.GetById(id);
        }

        public Restaurant InsertRestaurant(Restaurant restaurant)
        {
            return _restaurantRepository.Insert(restaurant);
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            return _restaurantRepository.Update(restaurant);
        }

        public void DeleteRestaurant(long id)
        {
            _restaurantRepository.Delete(id);
        }
    }
}