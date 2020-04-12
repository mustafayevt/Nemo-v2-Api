using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;
using Npgsql;

namespace Nemo_v2_Service.Services
{
    public class WarehouseExportInvoiceService : IWarehouseExportInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IIngredientService _ingredientService;

        public WarehouseExportInvoiceService(IUnitOfWork unitOfWork, IIngredientService ingredientService)
        {
            _unitOfWork = unitOfWork;
            _ingredientService = ingredientService;
        }

        public IEnumerable<WarehouseExportInvoice> Get()
        {
            return _unitOfWork.WarehouseExportInvoiceRepository.Get();
        }

        public IEnumerable<WarehouseExportInvoice> GetWarehouseExportInvoiceByRestaurantId(long RestId)
        {
            return _unitOfWork.WarehouseExportInvoiceRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.IngredientsExports);
        }

        public WarehouseExportInvoice GetWarehouseExportInvoice(long id)
        {
            return _unitOfWork.WarehouseExportInvoiceRepository.Query(x => x.Id == id)
                .Include(x => x.IngredientsExports).First();
        }

        public WarehouseExportInvoice InsertWarehouseExportInvoice(WarehouseExportInvoice WarehouseExportInvoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                // var a =_ingredientService.CalculateAveragePrice(38,9);
                if (WarehouseExportInvoice.IngredientsExports?.Any() ?? false)
                {
                    if (WarehouseExportInvoice.IngredientsExports.Any(x => x.IngredientId == 0))
                        throw new NullReferenceException("Ingredient not found");
                    var warehouse = _unitOfWork.WarehouseRepository
                        .Query(x => x.Id == WarehouseExportInvoice.WarehouseId)
                        .Include(x => x.IngredientWarehouseRels).First();

                    var ingredientWareRel = WarehouseExportInvoice.IngredientsExports.Select(y => y.IngredientId)
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

                WarehouseExportInvoice.TotalAmount = 0;
                foreach (var ingredientsExport in WarehouseExportInvoice.IngredientsExports)
                {
                    WarehouseExportInvoice.TotalAmount +=
                        ingredientsExport.Quantity * _ingredientService.CalculateAveragePrice(
                            ingredientsExport.IngredientId,
                            WarehouseExportInvoice.WarehouseId);
                }

                var warehouseExportInvoice =
                    _unitOfWork.WarehouseExportInvoiceRepository.Insert(WarehouseExportInvoice);
                _ingredientService.ExportIngredient(WarehouseExportInvoice.IngredientsExports);
                _unitOfWork.Save();
                _unitOfWork.Commit();

                return warehouseExportInvoice;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public WarehouseExportInvoice UpdateWarehouseExportInvoice(WarehouseExportInvoice WarehouseExportInvoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.WarehouseExportInvoiceRepository.Update(WarehouseExportInvoice);
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

        public void DeleteWarehouseExportInvoice(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.WarehouseExportInvoiceRepository.Delete(id);
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