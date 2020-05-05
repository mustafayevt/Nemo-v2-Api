using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class WarehouseTransferInvoiceService : IWarehouseTransferInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IIngredientService _ingredientService;

        public IEnumerable<WarehouseTransferInvoice> Get()
        {
            return _unitOfWork.WarehouseTransferInvoiceRepository.Get();
        }

        public IEnumerable<WarehouseTransferInvoice> GetWarehouseTransferInvoiceByRestaurantId(long RestId)
        {
            return _unitOfWork.WarehouseTransferInvoiceRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.IngredientsTransfers);
        }

        public WarehouseTransferInvoice GetWarehouseTransferInvoice(long id)
        {
            return _unitOfWork.WarehouseTransferInvoiceRepository.Query(x => x.Id == id)
                .Include(x => x.IngredientsTransfers).First();
        }

        public WarehouseTransferInvoice InsertWarehouseTransferInvoice(
            WarehouseTransferInvoice WarehouseTransferInvoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                // var a =_ingredientService.CalculateAveragePrice(38,9);
                if (WarehouseTransferInvoice.IngredientsTransfers?.Any() ?? false)
                {
                    if (WarehouseTransferInvoice.IngredientsTransfers.Any(x => x.IngredientId == 0))
                        throw new NullReferenceException("Ingredient not found");
                    var warehouse = _unitOfWork.WarehouseRepository
                        .Query(x => x.Id == WarehouseTransferInvoice.WarehouseId)
                        .Include(x => x.IngredientWarehouseRels).First();

                    var ingredientWareRel = WarehouseTransferInvoice.IngredientsTransfers.Select(y => y.IngredientId)
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

                WarehouseTransferInvoice.TotalAmount = 0;
                foreach (var ingredientsExport in WarehouseTransferInvoice.IngredientsTransfers)
                {
                    WarehouseTransferInvoice.TotalAmount +=
                        ingredientsExport.Quantity * _ingredientService.CalculateAveragePrice(
                            ingredientsExport.IngredientId,
                            WarehouseTransferInvoice.WarehouseId);
                }

                var warehouseTransferInvoice =
                    _unitOfWork.WarehouseTransferInvoiceRepository.Insert(WarehouseTransferInvoice);
                _ingredientService.DecreaseIngredientQuantity(WarehouseTransferInvoice.IngredientsTransfers.Select(x =>
                    new IngredientWarehouseRel()
                    {
                        IngredientId = x.IngredientId,
                        WarehouseId = x.WarehouseTransferInvoice.WarehouseId,
                        Quantity = x.Quantity
                    }
                ));
                _unitOfWork.Save();
                _unitOfWork.Commit();

                return warehouseTransferInvoice;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public WarehouseTransferInvoice UpdateWarehouseTransferInvoice(
            WarehouseTransferInvoice WarehouseTransferInvoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.WarehouseTransferInvoiceRepository.Update(WarehouseTransferInvoice);
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

        public void DeleteWarehouseTransferInvoice(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.WarehouseTransferInvoiceRepository.Delete(id);
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