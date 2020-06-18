using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> Get();  
        IEnumerable<Invoice> GetInvoicesByRestaurantId(long RestId);  
        Invoice GetInvoice(long id);  
        Invoice InsertInvoice(Invoice invoice);
        void ReduceIngredientsInInvoices(ICollection<Invoice> invoices);
        Task<int> ReduceIngredientsInInvoiceByDate(long restId,DateTime startDate, DateTime endDate);
        Invoice UpdateInvoice(Invoice invoice);  
        void DeleteInvoice(long id);  
    }
}