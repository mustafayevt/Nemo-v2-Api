using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class WarehouseInvoiceService:IWarehouseInvoiceService
    {
        private IRepository<WarehouseInvoice> _warehouseInvoiceRepository;
        private IRepository<IngredientsInsert> _ingredientsInsertRepository;

        public WarehouseInvoiceService(IRepository<WarehouseInvoice> warehouseInvoiceRepository,
            IRepository<IngredientsInsert> ingredientsInsertRepository)
        {
            _warehouseInvoiceRepository = warehouseInvoiceRepository;
            _ingredientsInsertRepository = ingredientsInsertRepository;
        }

        public IEnumerable<WarehouseInvoice> Get()
        {
            return _warehouseInvoiceRepository.Get();
        }

        public IEnumerable<WarehouseInvoice> GetWarehouseInvoiceByRestaurantId(long RestId)
        {
            return _warehouseInvoiceRepository.Query(x => x.RestaurantId == RestId);
        }

        public WarehouseInvoice GetWarehouseInvoice(long id)
        {
            return _warehouseInvoiceRepository.GetById(id);
        }

        public WarehouseInvoice InsertWarehouseInvoice(WarehouseInvoice WarehouseInvoice)
        {
            return _warehouseInvoiceRepository.Insert(WarehouseInvoice);
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