using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IWarehouseInvoiceService
    {
        IEnumerable<WarehouseInvoice> Get();  
        IEnumerable<WarehouseInvoice> GetWarehouseInvoiceByRestaurantId(long RestId);  
        WarehouseInvoice GetWarehouseInvoice(long id);  
        WarehouseInvoice InsertWarehouseInvoice(WarehouseInvoice WarehouseInvoice);  
        WarehouseInvoice UpdateWarehouseInvoice(WarehouseInvoice WarehouseInvoice);  
        void DeleteWarehouseInvoice(long id);
    }
}