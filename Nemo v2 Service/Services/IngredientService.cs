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
        private readonly IRepository<IngredientsInsert> _ingredientsInsertRepository;

        public IngredientService(IRepository<Ingredient> ingredientRepository,
            IRepository<IngredientCategory> ingredientCategoryRepository,
            IRepository<IngredientsInsert> ingredientsInsertRepository)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientCategoryRepository = ingredientCategoryRepository;
            _ingredientsInsertRepository = ingredientsInsertRepository;
        }

        public IEnumerable<Ingredient> Get()
        {
            return _ingredientRepository.Get();
        }

        public IEnumerable<Ingredient> GetIngredientByRestaurantId(long RestId)
        {
            return _ingredientRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.IngredientCategories)
                .ThenInclude(x => x.IngredientCategory);
        }

        public Ingredient GetIngredient(long id)
        {
            return _ingredientRepository.Query(x => x.Id == id)
                .Include(x => x.IngredientCategories)
                .ThenInclude(x => x.IngredientCategory).First();
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

        public IEnumerable<Ingredient> IncreaseCurrentQuantity(IEnumerable<IngredientsInsert> ingredientsInserts)
        {
            var ingredientIds = ingredientsInserts.GroupBy(x => x.IngredientId);
            var ingredients = new List<Ingredient>();
            foreach (var insert in ingredientIds)
            {
                ingredients.Add(_ingredientRepository.GetById(insert.Key));
                foreach (var eachIngredient in insert)
                {
                    ingredients.Last().CurrentQuantity += eachIngredient.Quantity;
                }
            }

            return _ingredientRepository.UpdateMany(ingredients);
        }

        public decimal CalculateAveragePrice(long id)
        {
            var ingredient = _ingredientRepository.GetById(id);
            var inserts = _ingredientsInsertRepository.Query(x => x.IngredientId == id)
                .ToList();
            decimal PriceAmount = 0;
            if (ingredient.CurrentQuantity <= 0)
            {
                inserts.ForEach(x => PriceAmount += x.PriceForEach);
                return PriceAmount / inserts.Count();
            }

            decimal insertsQuantity = 0;
            inserts.ForEach(x => insertsQuantity += x.Quantity);
            var unAvailableQuantity = insertsQuantity - ingredient.CurrentQuantity;

            decimal count = 0;
            int index = 0;
            while (count <= unAvailableQuantity)
            {
                count += inserts[index++].Quantity;
            }

            var TakeSkip = inserts.Skip(index- 1).Take(inserts.Count());
            foreach (var ingredientsInsert in TakeSkip)
            {
                PriceAmount += ingredientsInsert.PriceForEach;
            }
            return PriceAmount / TakeSkip.Count();
        }

        public void DeleteIngredient(long id)
        {
            _ingredientRepository.Delete(id);
        }
    }
}