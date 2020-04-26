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
        private readonly IUnitOfWork _unitOfWork;

        public FoodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Food> Get()
        {
            return _unitOfWork.FoodRepository.Get();
        }

        public IEnumerable<Food> GetFoodByRestaurantId(long RestId)
        {
            return _unitOfWork.FoodRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.Ingredients).ThenInclude(y => y.Ingredient).ThenInclude(h => h.IngredientCategories)
                .ThenInclude(g => g.IngredientCategory)
                .Include(x => x.FoodGroups).ThenInclude(y => y.FoodGroup);
        }

        public Food GetFood(long id)
        {
            return _unitOfWork.FoodRepository.Query(x => x.Id == id)
                .Include(x => x.Ingredients).ThenInclude(y => y.Ingredient).ThenInclude(h => h.IngredientCategories)
                .ThenInclude(g => g.IngredientCategory)
                .Include(x => x.FoodGroups).ThenInclude(y => y.FoodGroup).First();
        }

        public Food InsertFood(Food Food)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Food.Ingredients?.Any() ?? false)
                {
                    if (Food.Ingredients.Any(x => x.IngredientId == 0))
                        throw new NullReferenceException("Ingredient not found");

                    var ingredientIds = Food.Ingredients.Select(y => y.IngredientId).ToList();
                    if (ingredientIds.Any())
                    {
                        var ingredients = _unitOfWork.IngredientRepository.Query(x => ingredientIds.Contains(x.Id))
                            .ToList();

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
                                Unit = Food.Ingredients[i].Unit
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
                        newGroup = _unitOfWork.FoodGroupRepository
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

                var result = _unitOfWork.FoodRepository.Insert(Food);
                _unitOfWork.Save();
                Food.FoodPrinterAndSectionRels.ForEach(x => { x.FoodId = Food.Id; });
                _unitOfWork.FoodRepository.Update(Food);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public Food UpdateFood(Food Food)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Food.Ingredients?.Any() ?? false)
                {
                    if (Food.Ingredients.Any(x => x.IngredientId == 0))
                        throw new NullReferenceException("Ingredient not found");

                    var ingredientIds = Food.Ingredients.Select(y => y.IngredientId).ToList();
                    if (ingredientIds.Any())
                    {
                        var ingredients = _unitOfWork.IngredientRepository.Query(x => ingredientIds.Contains(x.Id))
                            .ToList();

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
                                Unit = Food.Ingredients[i].Unit
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
                        newGroup = _unitOfWork.FoodGroupRepository
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

                Food.FoodPrinterAndSectionRels.ForEach(x => x.FoodId = Food.Id);
                var result = _unitOfWork.FoodRepository.Update(Food);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void DeleteFood(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.FoodRepository.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}