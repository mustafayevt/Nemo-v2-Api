using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public IngredientService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            _unitOfWork = unitOfWork;
            _applicationContext = applicationContext;
        }

        public IEnumerable<Ingredient> Get()
        {
            return _unitOfWork.IngredientRepository.Get();
        }

        public IEnumerable<Ingredient> GetIngredientByRestaurantId(long RestId)
        {
            return _unitOfWork.IngredientRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.IngredientCategories)
                .ThenInclude(x => x.IngredientCategory);
        }

        public IEnumerable<Ingredient> GetIngredientByWarehouseId(long WarehouseId)
        {
            return _unitOfWork.IngredientRepository
                .Query(x => x.IngredientWarehouseRels.Count(y => y.WarehouseId == WarehouseId) > 0)
                .Include(x => x.IngredientCategories)
                .ThenInclude(x => x.IngredientCategory);
        }

        public Ingredient GetIngredient(long id)
        {
            return _unitOfWork.IngredientRepository.Query(x => x.Id == id)
                .Include(x => x.IngredientCategories)
                .ThenInclude(x => x.IngredientCategory).First();
        }

        public Ingredient InsertIngredient(Ingredient Ingredient)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Ingredient.IngredientWarehouseRels?.Any() ?? false)
                {
                    if (Ingredient.IngredientWarehouseRels.Any(x => x.Warehouse.Id == 0))
                        throw new NullReferenceException("Warehouse not found");

                    var warehouseIds = Ingredient.IngredientWarehouseRels.Select(y => y.Warehouse.Id).ToList();
                    if (warehouseIds.Any())
                    {
                        var warehouses = _unitOfWork.WarehouseRepository.Query(x => warehouseIds.Contains(x.Id)).ToList();

                        if (warehouses.Count() != Ingredient.IngredientWarehouseRels.Count())
                            throw new NullReferenceException("Warehouse Not Found");
                        Ingredient.IngredientWarehouseRels.Clear();
                        warehouses.ForEach(x =>
                        {
                            Ingredient.IngredientWarehouseRels.Add(new IngredientWarehouseRel()
                            {
                                IngredientId = Ingredient.Id,
                                WarehouseId = x.Id,
                                Quantity = 0
                            });
                        });
                    }
                }


                if (Ingredient.IngredientCategories?.Any() ?? false)
                {
                    if (Ingredient.IngredientCategories.Any(x => x.IngredientCategory.Id == 0))
                    {
                        IEnumerable<IngredientCategory> newCategories;
                        newCategories = _unitOfWork.IngredientCategoryRepository
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

                var result = _unitOfWork.IngredientRepository.Insert(Ingredient);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public Ingredient UpdateIngredient(Ingredient Ingredient)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Ingredient.IngredientWarehouseRels?.Any() ?? false)
                {
                    if (Ingredient.IngredientWarehouseRels.Any(x => x.Warehouse.Id == 0))
                        throw new NullReferenceException("Warehouse not found");

                    var warehouseIds = Ingredient.IngredientWarehouseRels.Select(y => y.Warehouse.Id).ToList();
                    if (warehouseIds.Any())
                    {
                        var warehouses = _unitOfWork.IngredientRepository.Query(x => warehouseIds.Contains(x.Id)).ToList();

                        if (warehouses.Count() != Ingredient.IngredientWarehouseRels.Count())
                            throw new NullReferenceException("Warehouse Not Found");
                        Ingredient.IngredientWarehouseRels.Clear();
                        warehouses.ForEach(x =>
                        {
                            Ingredient.IngredientWarehouseRels.Add(new IngredientWarehouseRel()
                            {
                                IngredientId = Ingredient.Id,
                                WarehouseId = x.Id
                            });
                        });
                    }
                }


                if (Ingredient.IngredientCategories?.Any() ?? false)
                {
                    if (Ingredient.IngredientCategories.Any(x => x.IngredientCategory.Id == 0))
                    {
                        IEnumerable<IngredientCategory> newCategories;
                        newCategories = _unitOfWork.IngredientCategoryRepository
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

                var result =  _unitOfWork.IngredientRepository.Update(Ingredient);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public IEnumerable<Ingredient> InsertIngredient(IEnumerable<IngredientsInsert> ingredientsInserts)
        {
            try
            {
                var warehouses = ingredientsInserts.GroupBy(x => x.WarehouseInvoice.WarehouseId);
                var ingredients = new List<Ingredient>();
                foreach (var warehouse in warehouses)
                {
                    ingredients.AddRange(_unitOfWork.IngredientRepository
                        .Query(x => x.IngredientWarehouseRels.Count(y => y.WarehouseId == warehouse.Key) > 0)
                        .Include(x => x.IngredientWarehouseRels));
                }

                foreach (var ingredientsInsert in ingredientsInserts)
                {
                    var ingredient = ingredients.FirstOrDefault(x =>
                        x.Id == ingredientsInsert.IngredientId).IngredientWarehouseRels.First(x =>
                        x.WarehouseId == ingredientsInsert.WarehouseInvoice.WarehouseId);

                    ingredient.Quantity += ingredientsInsert.Quantity;
                }

                var result =  _unitOfWork.IngredientRepository.UpdateMany(ingredients);
                _unitOfWork.Save();
                return result;
            }
            catch (Exception e)
            {
                throw ;
            }
        }

        public IEnumerable<Ingredient> ExportIngredient(IEnumerable<IngredientsExport> ingredientsExports)
        {
            try
            {
                var warehouses = ingredientsExports.GroupBy(x => x.WarehouseExportInvoice.WarehouseId);
                var ingredients = new List<Ingredient>();
                foreach (var warehouse in warehouses)
                {
                    ingredients.AddRange(_unitOfWork.IngredientRepository
                        .Query(x => x.IngredientWarehouseRels.Count(y => y.WarehouseId == warehouse.Key) > 0)
                        .Include(x => x.IngredientWarehouseRels));
                }

                foreach (var ingredientsExport in ingredientsExports)
                {
                    var ingredient = ingredients.AsQueryable().FirstOrDefault(x =>
                        x.Id == ingredientsExport.IngredientId).IngredientWarehouseRels.First(x =>
                        x.WarehouseId == ingredientsExport.WarehouseExportInvoice.WarehouseId);

                    ingredient.Quantity -= ingredientsExport.Quantity;
                }

                var result =  _unitOfWork.IngredientRepository.UpdateMany(ingredients);
                _unitOfWork.Save();
                return result;
            }
            catch (Exception e)
            {
                throw ;
            }
        }

        public IEnumerable<Ingredient> IncreaseIngredientQuantity(IEnumerable<IngredientWarehouseRel> ingredientWarehouseRels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var warehouses = ingredientWarehouseRels.GroupBy(x => x.WarehouseId);
                var ingredients = new List<Ingredient>();
                foreach (var warehouse in warehouses)
                {
                    ingredients.AddRange(_unitOfWork.IngredientRepository
                        .Query(x => x.IngredientWarehouseRels.Count(y => y.WarehouseId == warehouse.Key) > 0)
                        .Include(x => x.IngredientWarehouseRels));
                }

                foreach (var ingredientsExport in ingredientWarehouseRels)
                {
                    var ingredient = ingredients.AsQueryable().FirstOrDefault(x =>
                        x.Id == ingredientsExport.IngredientId).IngredientWarehouseRels.First(x =>
                        x.WarehouseId == ingredientsExport.WarehouseId);

                    ingredient.Quantity += ingredientsExport.Quantity;
                }

                var result =  _unitOfWork.IngredientRepository.UpdateMany(ingredients);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public IEnumerable<Ingredient> DecreaseIngredientQuantity(IEnumerable<IngredientWarehouseRel> ingredientsExports)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var warehouses = ingredientsExports.GroupBy(x => x.WarehouseId);
                var ingredients = new List<Ingredient>();
                foreach (var warehouse in warehouses)
                {
                    ingredients.AddRange(_unitOfWork.IngredientRepository
                        .Query(x => x.IngredientWarehouseRels.Count(y => y.WarehouseId == warehouse.Key) > 0)
                        .Include(x => x.IngredientWarehouseRels));
                }

                foreach (var ingredientsExport in ingredientsExports)
                {
                    var ingredient = ingredients.AsQueryable().FirstOrDefault(x =>
                        x.Id == ingredientsExport.IngredientId).IngredientWarehouseRels.First(x =>
                        x.WarehouseId == ingredientsExport.WarehouseId);

                    ingredient.Quantity -= ingredientsExport.Quantity;
                }

                var result =  _unitOfWork.IngredientRepository.UpdateMany(ingredients);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }
        
        public decimal CalculateAveragePrice(long IngredientId, long WarehouseId)
        {
            var ingredient = _unitOfWork.IngredientRepository.Query(x => x.Id == IngredientId)
                .Include(y => y.IngredientWarehouseRels).First().IngredientWarehouseRels
                .FirstOrDefault(x => x.WarehouseId == WarehouseId);

            try
            {
                var inserts = _unitOfWork.IngredientsInsertRepository.Query(x => x.IngredientId == IngredientId)
                    .Include(x => x.WarehouseInvoice).Where(x => x.WarehouseInvoice.WarehouseId == WarehouseId)
                    .ToList();


                decimal PriceAmount = 0;
                if (ingredient.Quantity <= 0)
                {
                    inserts.ForEach(x => PriceAmount += x.Price);
                    return PriceAmount / inserts.Count();
                }

                decimal insertsQuantity = 0;
                inserts.ForEach(x => insertsQuantity += x.Quantity);
                var unAvailableQuantity = insertsQuantity - ingredient.Quantity;

                decimal count = 0;
                int index = 0;
                while (count <= unAvailableQuantity)
                {
                    count += inserts[index++].Quantity;
                }

                var TakeSkip = inserts.Skip(index - 1).Take(inserts.Count());
                foreach (var ingredientsInsert in TakeSkip)
                {
                    PriceAmount += ingredientsInsert.Price;
                }

                return PriceAmount / TakeSkip.Count();
            }
            catch (Exception e)
            {
                return 0;
            }

            // return 5;
        }

        public void DeleteIngredient(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.IngredientRepository.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }
    }
}