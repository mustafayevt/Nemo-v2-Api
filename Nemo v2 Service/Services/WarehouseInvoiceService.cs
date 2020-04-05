using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class WarehouseInvoiceService : IWarehouseInvoiceService
    {
        private IRepository<WarehouseInvoice> _warehouseInvoiceRepository;
        private IIngredientService _ingredientService;

        public WarehouseInvoiceService(IRepository<WarehouseInvoice> warehouseInvoiceRepository,
            IIngredientService ingredientService)
        {
            _warehouseInvoiceRepository = warehouseInvoiceRepository;
            _ingredientService = ingredientService;
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
            // var a =_ingredientService.CalculateAveragePrice(28);
            
            var warehouseInvoice = _warehouseInvoiceRepository.Insert(WarehouseInvoice);
            _ingredientService.IncreaseCurrentQuantity(WarehouseInvoice.IngredientsInserts);

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