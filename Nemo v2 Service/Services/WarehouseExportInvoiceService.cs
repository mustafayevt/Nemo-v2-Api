using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class WarehouseExportInvoiceService:IWarehouseExportInvoiceService
    {
        private IRepository<WarehouseExportInvoice> _warehouseExportInvoiceRepository;
        private IRepository<Ingredient> _ingredientRepository;
        private IRepository<Warehouse> _warehouseRepository;
        private IIngredientService _ingredientService;

        public WarehouseExportInvoiceService(IRepository<WarehouseExportInvoice> warehouseExportInvoiceRepository,
            IIngredientService ingredientService,
            IRepository<Ingredient> ingredientRepository,
            IRepository<Warehouse> warehouseRepository)
        {
            _warehouseExportInvoiceRepository = warehouseExportInvoiceRepository;
            _ingredientService = ingredientService;
            _ingredientRepository = ingredientRepository;
            _warehouseRepository = warehouseRepository;
        }

        public IEnumerable<WarehouseExportInvoice> Get()
        {
            return _warehouseExportInvoiceRepository.Get();
        }

        public IEnumerable<WarehouseExportInvoice> GetWarehouseExportInvoiceByRestaurantId(long RestId)
        {
            return _warehouseExportInvoiceRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.IngredientsExports);
        }

        public WarehouseExportInvoice GetWarehouseExportInvoice(long id)
        {
            return _warehouseExportInvoiceRepository.Query(x => x.Id == id)
                .Include(x => x.IngredientsExports).First();
        }

        public WarehouseExportInvoice InsertWarehouseExportInvoice(WarehouseExportInvoice WarehouseExportInvoice)
        {
            // var a =_ingredientService.CalculateAveragePrice(38,9);
            if (WarehouseExportInvoice.IngredientsExports?.Any() ?? false)
            {
                if (WarehouseExportInvoice.IngredientsExports.Any(x => x.IngredientId == 0))
                    throw new NullReferenceException("Ingredient not found");
                var warehouse = _warehouseRepository.Query(x => x.Id == WarehouseExportInvoice.WarehouseId)
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
                    _warehouseRepository.Update(warehouse);
                }
            }


            var warehouseExportInvoice = _warehouseExportInvoiceRepository.Insert(WarehouseExportInvoice);
            _ingredientService.ExportIngredient(WarehouseExportInvoice.IngredientsExports);

            return warehouseExportInvoice;
        }

        public WarehouseExportInvoice UpdateWarehouseExportInvoice(WarehouseExportInvoice WarehouseExportInvoice)
        {
            return _warehouseExportInvoiceRepository.Update(WarehouseExportInvoice);
        }

        public void DeleteWarehouseExportInvoice(long id)
        {
            _warehouseExportInvoiceRepository.Delete(id);
        }
    }
}