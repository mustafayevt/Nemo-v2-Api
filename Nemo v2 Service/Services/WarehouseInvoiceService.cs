using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.Helper;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class WarehouseInvoiceService : IWarehouseInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIngredientService _ingredientService;

        public WarehouseInvoiceService(IUnitOfWork unitOfWork, IIngredientService ingredientService)
        {
            _unitOfWork = unitOfWork;
            _ingredientService = ingredientService;
        }

        public IEnumerable<WarehouseInvoice> Get()
        {
            return _unitOfWork.WarehouseInvoiceRepository.Get();
        }

        public IEnumerable<WarehouseInvoice> GetWarehouseInvoiceByRestaurantId(long RestId)
        {
            return _unitOfWork.WarehouseInvoiceRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.IngredientsInserts);
        }

        public WarehouseInvoice GetWarehouseInvoice(long id)
        {
            return _unitOfWork.WarehouseInvoiceRepository.Query(x => x.Id == id)
                .Include(x => x.IngredientsInserts).First();
        }

        public WarehouseInvoice InsertWarehouseInvoice(WarehouseInvoice WarehouseInvoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                // var a =_ingredientService.CalculateAveragePrice(38,9);
                if (WarehouseInvoice.IngredientsInserts?.Any() ?? false)
                {
                    if (WarehouseInvoice.IngredientsInserts.Any(x => x.IngredientId == 0))
                        throw new NullReferenceException("Ingredient not found");
                    var warehouse = _unitOfWork.WarehouseRepository.Query(x => x.Id == WarehouseInvoice.WarehouseId)
                        .Include(x => x.IngredientWarehouseRels).First();

                    var ingredientWareRel = WarehouseInvoice.IngredientsInserts.Select(y => y.IngredientId)
                        .Except(warehouse.IngredientWarehouseRels.Select(y => y.IngredientId)).ToList();

                    if (ingredientWareRel?.Any() ?? false)
                    {
                        foreach (var ingredient in ingredientWareRel)
                        {
                            warehouse.IngredientWarehouseRels.Add(new IngredientWarehouseRel()
                            {
                                IngredientId = ingredient,
                                WarehouseId = warehouse.Id,
                                Quantity = 0
                            });
                        }
                        _unitOfWork.WarehouseRepository.Update(warehouse);
                        _unitOfWork.Save();
                    }
                }


                var warehouseInvoice = _unitOfWork.WarehouseInvoiceRepository.Insert(WarehouseInvoice);
                _unitOfWork.Save();
                var lastComputedNumber = _unitOfWork.WarehouseInvoiceRepository
                    .Query(x => x.RestaurantId == WarehouseInvoice.RestaurantId)
                    .OrderBy(y => y.ComputedNumber).Last();
                WarehouseInvoice.ComputedNumber = lastComputedNumber !=null ? lastComputedNumber.ComputedNumber+1 : 1;
                _ingredientService.InsertIngredient(WarehouseInvoice.IngredientsInserts);
                _unitOfWork.Save();
                _unitOfWork.Commit();

                return warehouseInvoice;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public WarehouseInvoice UpdateWarehouseInvoice(WarehouseInvoice WarehouseInvoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.WarehouseInvoiceRepository.Update(WarehouseInvoice);
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

        public void DeleteWarehouseInvoice(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.WarehouseInvoiceRepository.Delete(id);
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