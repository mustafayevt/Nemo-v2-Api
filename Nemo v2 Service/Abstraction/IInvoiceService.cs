using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> Get();  
        IEnumerable<Invoice> GetInvoicesByRestaurantId(long RestId);  
        Invoice GetInvoice(long id);  
        Invoice InsertInvoice(Invoice invoice);  
        Invoice UpdateInvoice(Invoice invoice);  
        void DeleteInvoice(long id);  
    }
}