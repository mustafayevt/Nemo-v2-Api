using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class IngredientCategoryService:IIngredientCategoryService
    {
        private IRepository<IngredientCategory> _ingredientCategoryRepository;

        public IngredientCategoryService(IRepository<IngredientCategory> ingredientCategoryRepository)
        {
            _ingredientCategoryRepository = ingredientCategoryRepository;
        }

        public IEnumerable<IngredientCategory> Get()
        {
           return _ingredientCategoryRepository.Get();
        }

        public IEnumerable<IngredientCategory> GetIngredientCategoryByRestaurantId(long RestId)
        {
            return _ingredientCategoryRepository.Query(x => x.RestaurantId == RestId);
        }

        public IngredientCategory GetIngredientCategory(long id)
        {
            return _ingredientCategoryRepository.GetById(id);
        }

        public IngredientCategory InsertIngredientCategory(IngredientCategory IngredientCategory)
        {
            
            return _ingredientCategoryRepository.Insert(IngredientCategory);
        }

        public IngredientCategory UpdateIngredientCategory(IngredientCategory IngredientCategory)
        {
            return _ingredientCategoryRepository.Update(IngredientCategory);
        }

        public void DeleteIngredientCategory(long id)
        {
            _ingredientCategoryRepository.Delete(id);
        }
    }
}