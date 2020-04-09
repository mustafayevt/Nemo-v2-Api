using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IWarehouseExportInvoiceService
    {
        IEnumerable<WarehouseExportInvoice> Get();  
        IEnumerable<WarehouseExportInvoice> GetWarehouseExportInvoiceByRestaurantId(long RestId);  
        WarehouseExportInvoice GetWarehouseExportInvoice(long id);  
        WarehouseExportInvoice InsertWarehouseExportInvoice(WarehouseExportInvoice WarehouseExportInvoice);  
        WarehouseExportInvoice UpdateWarehouseExportInvoice(WarehouseExportInvoice WarehouseExportInvoice);  
        void DeleteWarehouseExportInvoice(long id);
    }
}