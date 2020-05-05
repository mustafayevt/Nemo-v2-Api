using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IWarehouseTransferInvoiceService
    {
        IEnumerable<WarehouseTransferInvoice> Get();  
        IEnumerable<WarehouseTransferInvoice> GetWarehouseTransferInvoiceByRestaurantId(long RestId);  
        WarehouseTransferInvoice GetWarehouseTransferInvoice(long id);  
        WarehouseTransferInvoice InsertWarehouseTransferInvoice(WarehouseTransferInvoice WarehouseTransferInvoice);  
        WarehouseTransferInvoice UpdateWarehouseTransferInvoice(WarehouseTransferInvoice WarehouseTransferInvoice);  
        void DeleteWarehouseTransferInvoice(long id);
    }
}