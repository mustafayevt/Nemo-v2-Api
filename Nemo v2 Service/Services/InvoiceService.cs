using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIngredientService _ingredientService;

        public InvoiceService(IUnitOfWork unitOfWork, IIngredientService ingredientService)
        {
            _unitOfWork = unitOfWork;
            _ingredientService = ingredientService;
        }

        public IEnumerable<Invoice> Get()
        {
            return _unitOfWork.InvoiceRepository.Get();
        }

        public IEnumerable<Invoice> GetInvoicesByRestaurantId(long RestId)
        {
            return _unitOfWork.InvoiceRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.Foods)
                .ThenInclude(x => x.Food);
        }

        public Invoice GetInvoice(long id)
        {
            return _unitOfWork.InvoiceRepository.Query(x => x.Id == id)
                .Include(x => x.Foods)
                .ThenInclude(x => x.Food).First();
        }

        public Invoice InsertInvoice(Invoice invoice)
        {
            using (var transaction = _unitOfWork.CreateTransaction())
            {
                try
                {
                    if (invoice.Foods?.Any() ?? false)
                    {
                        if (invoice.Foods.Any(x => x.FoodId == 0))
                            throw new NullReferenceException("Food not found");

                        // var foodIds = invoice.Foods.Select(y => y.FoodId).GroupBy(x=>x);
                        // if (foodIds.Any())
                        // {
                        //     //invoice.Ingredients.Clear();
                        //     var foodInvoiceRels = new List<FoodInvoiceRel>();
                        //     foreach (var foodId in foodIds)
                        //     {
                        //         foodInvoiceRels.Add(new FoodInvoiceRel()
                        //         {
                        //             FoodId = foodId.Key
                        //         });
                        //     }
                        //
                        //     invoice.Foods = foodInvoiceRels;
                        // }
                    }

                    if (invoice.InvoiceTableRels?.Any() ?? false)
                    {
                        if (invoice.InvoiceTableRels.Any(x => x.TableId == 0))
                            throw new NullReferenceException("Table not found");

                        var tableIds = invoice.InvoiceTableRels.Select(y => y.TableId).ToList();
                        if (tableIds.Any())
                        {
                            var tables = _unitOfWork.TableRepository.Query(x => tableIds.Contains(x.Id)).ToList();

                            if (tables.Count() != invoice.InvoiceTableRels.Count())
                                throw new NullReferenceException("Table Not Found");
                            //invoice.Ingredients.Clear();
                            var invoiceTableRels = new List<InvoiceTableRel>();
                            for (int i = 0; i < tables.Count(); i++)
                            {
                                invoiceTableRels.Add(new InvoiceTableRel()
                                {
                                    InvoiceId = invoice.Id,
                                    TableId = tables[i].Id,
                                });
                            }

                            invoice.InvoiceTableRels = invoiceTableRels;
                        }
                    }


                    var result = _unitOfWork.InvoiceRepository.Insert(invoice);

                    if (invoice.IsIngredientReduced)
                    {
                        ReduceIngredientsInInvoices(new List<Invoice>
                        {
                            invoice
                        });
                    }

                    _unitOfWork.Save();
                    transaction.Commit();
                    return result;
                }
                catch (Exception e)
                {
                    _unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public void ReduceIngredientsInInvoices(ICollection<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                var ingredients = new List<IngredientWarehouseRel>();
                foreach (var food in invoice.Foods.ToList())
                {
                    foreach (var foodInvoiceProperties in food.FoodInvoiceProperties)
                    {
                        for (int i = 0; i < foodInvoiceProperties.Count; i++)
                        {
                            var ingredientsInFoods = _unitOfWork.FoodRepository
                                .Query(y => y.Id == food.FoodId).Include(h => h.Ingredients)
                                .SelectMany(y => y.Ingredients);

                            foreach (var ingredient in ingredientsInFoods)
                            {
                                ingredients.Add(new IngredientWarehouseRel()
                                {
                                    IngredientId = ingredient.IngredientId,
                                    Quantity = ingredient.Quantity * (decimal) foodInvoiceProperties.Portion,
                                    WarehouseId = ingredient.WarehouseId
                                });
                            }
                        }
                    }
                }

                _ingredientService.DecreaseIngredientQuantity(ingredients);
            }
        }

        public async Task<int> ReduceIngredientsInInvoiceByDate(long restId, DateTime startDate, DateTime endDate)
        {
            using (var transaction = _unitOfWork.CreateTransaction())
            {
                try
                {
                    var invoices = _unitOfWork.InvoiceRepository.Query(x =>
                        x.RestaurantId == restId &&
                        x.IsIngredientReduced == false &&
                        x.AddedDate.Date >= startDate.Date &&
                        x.AddedDate <= endDate.Date).Include(y =>
                        y.Foods).ThenInclude(y =>
                        y.FoodInvoiceProperties);

                    var mustUpdateList = invoices.ToList();
                    
                    ReduceIngredientsInInvoices(mustUpdateList);

                    await invoices.ForEachAsync(x => x.IsIngredientReduced = true);

                    _unitOfWork.Save();
                    transaction.Commit();
                    
                    return mustUpdateList.Count();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Invoice UpdateInvoice(Invoice invoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (invoice.Foods?.Any() ?? false)
                {
                    if (invoice.Foods.Any(x => x.FoodId == 0))
                        throw new NullReferenceException("Food not found");

                    // var foodIds = invoice.Foods.Select(y => y.FoodId).GroupBy(x=>x);
                    // if (foodIds.Any())
                    // {
                    //   
                    //     //invoice.Ingredients.Clear();
                    //     var foodInvoiceRels = new List<FoodInvoiceRel>();
                    //     foreach (var foodId in foodIds)
                    //     {
                    //         foodInvoiceRels.Add(new FoodInvoiceRel()
                    //         {
                    //             FoodId = foodId.Key,
                    //             Count = foodId.Count()
                    //         });
                    //     }
                    //
                    //     invoice.Foods = foodInvoiceRels;
                    // }
                }

                if (invoice.InvoiceTableRels?.Any() ?? false)
                {
                    if (invoice.InvoiceTableRels.Any(x => x.TableId == 0))
                        throw new NullReferenceException("Table not found");

                    var tableIds = invoice.InvoiceTableRels.Select(y => y.TableId).ToList();
                    if (tableIds.Any())
                    {
                        var tables = _unitOfWork.TableRepository.Query(x => tableIds.Contains(x.Id)).ToList();

                        if (tables.Count() != invoice.InvoiceTableRels.Count())
                            throw new NullReferenceException("Table Not Found");
                        //invoice.Ingredients.Clear();
                        var invoiceTableRels = new List<InvoiceTableRel>();
                        for (int i = 0; i < tables.Count(); i++)
                        {
                            invoiceTableRels.Add(new InvoiceTableRel()
                            {
                                InvoiceId = invoice.Id,
                                TableId = tables[i].Id,
                            });
                        }

                        invoice.InvoiceTableRels = invoiceTableRels;
                    }
                }

                var result = _unitOfWork.InvoiceRepository.Update(invoice);
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

        public void DeleteInvoice(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.InvoiceRepository.Delete(id);
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