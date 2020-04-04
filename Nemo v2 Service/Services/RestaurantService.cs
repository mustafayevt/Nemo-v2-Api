using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            return _restaurantRepository.Query(x => x.BranchId == RestId)
                .Include(x=>x.Branches)
                .Include(x=>x.Sections)
                .Include(x=>x.Tables);
        }

        public Restaurant GetRestaurant(long id)
        {
            return _restaurantRepository.Query(x => x.Id == id)
                .Include(x => x.Branches).First();
        }

        public Restaurant GetParentByBranchId(long id)
        {
            return _restaurantRepository.Query(x => x.Id == GetRestaurant(id).BranchId).First();
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