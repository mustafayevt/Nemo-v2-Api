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
        private IRepository<WarehouseInvoice> _warehouseInvoiceRepository;
        private IRepository<Ingredient> _ingredientRepository;
        private IRepository<Warehouse> _warehouseRepository;
        private IIngredientService _ingredientService;

        public WarehouseInvoiceService(IRepository<WarehouseInvoice> warehouseInvoiceRepository,
            IIngredientService ingredientService,
            IRepository<Ingredient> ingredientRepository,
            IRepository<Warehouse> warehouseRepository)
        {
            _warehouseInvoiceRepository = warehouseInvoiceRepository;
            _ingredientService = ingredientService;
            _ingredientRepository = ingredientRepository;
            _warehouseRepository = warehouseRepository;
        }

        public IEnumerable<WarehouseInvoice> Get()
        {
            return _warehouseInvoiceRepository.Get();
        }

        public IEnumerable<WarehouseInvoice> GetWarehouseInvoiceByRestaurantId(long RestId)
        {
            return _warehouseInvoiceRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.IngredientsInserts);
        }

        public WarehouseInvoice GetWarehouseInvoice(long id)
        {
            return _warehouseInvoiceRepository.Query(x => x.Id == id)
                .Include(x => x.IngredientsInserts).First();
        }

        public WarehouseInvoice InsertWarehouseInvoice(WarehouseInvoice WarehouseInvoice)
        {
            // var a =_ingredientService.CalculateAveragePrice(38,9);
            if (WarehouseInvoice.IngredientsInserts?.Any() ?? false)
            {
                if (WarehouseInvoice.IngredientsInserts.Any(x => x.IngredientId == 0))
                    throw new NullReferenceException("Ingredient not found");
                var warehouse = _warehouseRepository.Query(x => x.Id == WarehouseInvoice.WarehouseId)
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
                    _warehouseRepository.Update(warehouse);
                }
            }


            var warehouseInvoice = _warehouseInvoiceRepository.Insert(WarehouseInvoice);
            _ingredientService.InsertIngredient(WarehouseInvoice.IngredientsInserts);

            return warehouseInvoice;
        }

        public WarehouseInvoice UpdateWarehouseInvoice(WarehouseInvoice WarehouseInvoice)
        {
            return _warehouseInvoiceRepository.Update(WarehouseInvoice);
        }

        public void DeleteWarehouseInvoice(long id)
        {
            _warehouseInvoiceRepository.Delete(id);
        }
    }
}