using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class FoodService : IFoodService
    {
        private IRepository<Food> _foodRepository;
        private IRepository<FoodGroup> _foodGroupRepository;
        private IRepository<Ingredient> _ingredientRepository;

        public FoodService(IRepository<Food> foodRepository,
            IRepository<FoodGroup> foodGroupRepository,
            IRepository<Ingredient> ingredientRepository)
        {
            _foodRepository = foodRepository;
            _foodGroupRepository = foodGroupRepository;
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<Food> Get()
        {
            return _foodRepository.Get();
        }

        public IEnumerable<Food> GetFoodByRestaurantId(long RestId)
        {
            return _foodRepository.Query(x => x.RestaurantId == RestId)
                .Include(x =>x.Ingredients).ThenInclude(y=>y.Ingredient).ThenInclude(h=>h.IngredientCategories).ThenInclude(g=>g.IngredientCategory)
                .Include(x=>x.FoodGroups).ThenInclude(y=>y.FoodGroup);
        }

        public Food GetFood(long id)
        {
            return _foodRepository.Query(x => x.Id == id)
                .Include(x =>x.Ingredients).ThenInclude(y=>y.Ingredient).ThenInclude(h=>h.IngredientCategories).ThenInclude(g=>g.IngredientCategory)
                .Include(x=>x.FoodGroups).ThenInclude(y=>y.FoodGroup).First();
        }

        public Food InsertFood(Food Food)
        {
            if (Food.Ingredients?.Any() ?? false)
            {
                if (Food.Ingredients.Any(x => x.Ingredient.Id == 0))
                    throw new NullReferenceException("Ingredient not found");

                var ingredientIds = Food.Ingredients.Select(y => y.Ingredient.Id).ToList();
                if (ingredientIds.Any())
                {
                    var ingredients = _ingredientRepository.Query(x => ingredientIds.Contains(x.Id)).ToList();

                    if (ingredients.Count() != Food.Ingredients.Count())
                        throw new NullReferenceException("Ingredient Not Found");
                    //Food.Ingredients.Clear();
                    var ingredientFoodRels = new List<IngredientFoodRel>();
                    for (int i = 0; i < ingredients.Count(); i++)
                    {
                        ingredientFoodRels.Add(new IngredientFoodRel()
                        {
                            FoodId = Food.Id,
                            IngredientId = ingredients[i].Id,
                            Quantity = Food.Ingredients[i].Quantity,
                            Unit =  Food.Ingredients[i].Unit
                        });
                    }

                    Food.Ingredients = ingredientFoodRels;
                }
            }


            if (Food.FoodGroups?.Any() ?? false)
            {
                if (Food.FoodGroups.Any(x => x.FoodGroup.Id == 0))
                {
                    IEnumerable<FoodGroup> newGroup;
                    newGroup = _foodGroupRepository
                        .InsertMany(Food.FoodGroups
                            .Where(x => x.FoodGroup.Id == 0)
                            .Select(x => x.FoodGroup)).ToList();

                    Food.FoodGroups.RemoveAll(x => x.FoodGroup.Id == 0);
                }

                Food.FoodGroups.ForEach(x =>
                {
                    x.FoodId = Food.Id;
                    x.FoodGroupId = x.FoodGroup.Id;
                    x.FoodGroup = null;
                });
            }


            return _foodRepository.Insert(Food);
        }

        public Food UpdateFood(Food Food)
        {
            if (Food.Ingredients?.Any() ?? false)
            {
                if (Food.Ingredients.Any(x => x.Ingredient.Id == 0))
                    throw new NullReferenceException("Ingredient not found");

                var ingredientIds = Food.Ingredients.Select(y => y.Ingredient.Id).ToList();
                if (ingredientIds.Any())
                {
                    var ingredients = _ingredientRepository.Query(x => ingredientIds.Contains(x.Id)).ToList();

                    if (ingredients.Count() != Food.Ingredients.Count())
                        throw new NullReferenceException("Ingredient Not Found");
                    Food.Ingredients.Clear();
                    ingredients.ForEach(x =>
                    {
                        Food.Ingredients.Add(new IngredientFoodRel()
                        {
                            FoodId = Food.Id,
                            IngredientId = x.Id
                        });
                    });
                }
            }


            if (Food.FoodGroups?.Any() ?? false)
            {
                if (Food.FoodGroups.Any(x => x.FoodGroup.Id == 0))
                {
                    IEnumerable<FoodGroup> newGroup;
                    newGroup = _foodGroupRepository
                        .InsertMany(Food.FoodGroups
                            .Where(x => x.FoodGroup.Id == 0)
                            .Select(x => x.FoodGroup)).ToList();

                    Food.FoodGroups.RemoveAll(x => x.FoodGroup.Id == 0);
                }

                Food.FoodGroups.ForEach(x =>
                {
                    x.FoodId = Food.Id;
                    x.FoodGroupId = x.FoodGroup.Id;
                    x.FoodGroup = null;
                });
            }

            return _foodRepository.Update(Food);
        }

        public void DeleteFood(long id)
        {
            _foodRepository.Delete(id);
        }
    }
}