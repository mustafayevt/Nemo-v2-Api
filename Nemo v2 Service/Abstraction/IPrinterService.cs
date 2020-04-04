using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IPrinterService
    {
        IEnumerable<Printer> Get();  
        IEnumerable<Printer> GetPrinterByRestaurantId(long RestId);  
        Printer GetPrinter(long id);  
        Printer InsertPrinter(Printer Printer);  
        Printer UpdatePrinter(Printer Printer);  
        void DeletePrinter(long id); 
    }
}