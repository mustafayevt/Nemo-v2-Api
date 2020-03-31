using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepository<Ingredient> _ingredientRepository;
        private readonly IRepository<IngredientCategory> _ingredientCategoryRepository;

        public IngredientService(IRepository<Ingredient> ingredientRepository,
            IRepository<IngredientCategory> ingredientCategoryRepository)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientCategoryRepository = ingredientCategoryRepository;
        }

        public IEnumerable<Ingredient> Get()
        {
            return _ingredientRepository.Get();
        }

        public IEnumerable<Ingredient> GetIngredientByRestaurantId(long RestId)
        {
            return _ingredientRepository.Query(x => x.RestaurantId == RestId)
                .Include(x=>x.IngredientCategories)
                .ThenInclude(x=>x.IngredientCategory);
        }

        public Ingredient GetIngredient(long id)
        {
            return _ingredientRepository.Query(x => x.Id == id)
                .Include(x=>x.IngredientCategories)
                .ThenInclude(x=>x.IngredientCategory).First();
        }

        public Ingredient InsertIngredient(Ingredient Ingredient)
        {
            if (Ingredient.IngredientCategories?.Any() ?? false)
            {
                if (Ingredient.IngredientCategories.Any(x => x.IngredientCategory.Id == 0))
                {
                    IEnumerable<IngredientCategory> newCategories;
                    newCategories = _ingredientCategoryRepository
                        .InsertMany(Ingredient.IngredientCategories
                            .Where(x => x.IngredientCategory.Id == 0)
                            .Select(x => x.IngredientCategory)).ToList();

                    Ingredient.IngredientCategories.RemoveAll(x => x.IngredientCategory.Id == 0);
                }

                Ingredient.IngredientCategories.ForEach(x =>
                {
                    x.IngredientId = Ingredient.Id;
                    x.IngredientCategoryId = x.IngredientCategory.Id;
                    x.IngredientCategory = null;
                });
            }

            return _ingredientRepository.Insert(Ingredient);
        }

        public Ingredient UpdateIngredient(Ingredient Ingredient)
        {
            if (Ingredient.IngredientCategories?.Any() ?? false)
            {
                if (Ingredient.IngredientCategories.Any(x => x.IngredientCategory.Id == 0))
                {
                    IEnumerable<IngredientCategory> newCategories;
                    newCategories = _ingredientCategoryRepository
                        .InsertMany(Ingredient.IngredientCategories
                            .Where(x => x.IngredientCategory.Id == 0)
                            .Select(x => x.IngredientCategory)).ToList();

                    Ingredient.IngredientCategories.RemoveAll(x => x.IngredientCategory.Id == 0);
                }

                Ingredient.IngredientCategories.ForEach(x =>
                {
                    x.IngredientId = Ingredient.Id;
                    x.IngredientCategoryId = x.IngredientCategory.Id;
                    x.IngredientCategory = null;
                });
            }

            return _ingredientRepository.Update(Ingredient);
        }

        public void DeleteIngredient(long id)
        {
            _ingredientRepository.Delete(id);
        }
    }
}