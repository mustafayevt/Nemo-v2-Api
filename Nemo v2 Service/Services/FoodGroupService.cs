using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class FoodGroupService:IFoodGroupService
    {
        private IRepository<FoodGroup> _foodGroupRepository;

        public FoodGroupService(IRepository<FoodGroup> foodGroupRepository)
        {
            _foodGroupRepository = foodGroupRepository;
        }

        public IEnumerable<FoodGroup> Get()
        {
            return _foodGroupRepository.Get();
        }

        public IEnumerable<FoodGroup> GetFoodGroupByRestaurantId(long RestId)
        {
            return _foodGroupRepository.Query(x => x.RestaurantId == RestId);
        }

        public FoodGroup GetFoodGroup(long id)
        {
            return _foodGroupRepository.GetById(id);
        }

        public FoodGroup InsertFoodGroup(FoodGroup FoodGroup)
        {
            return _foodGroupRepository.Insert(FoodGroup);
        }

        public FoodGroup UpdateFoodGroup(FoodGroup FoodGroup)
        {
            return _foodGroupRepository.Update(FoodGroup);
        }

        public void DeleteFoodGroup(long id)
        {
            _foodGroupRepository.Delete(id);
        }
    }
}