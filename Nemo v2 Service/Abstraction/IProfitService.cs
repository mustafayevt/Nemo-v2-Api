using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IProfitService
    {
        IEnumerable<Profit> Get();  
        IEnumerable<Profit> GetProfitByRestaurantId(long RestId);  
        Profit GetProfit(long id);  
        Profit InsertProfit(Profit Profit);  
        Profit UpdateProfit(Profit Profit);  
        void DeleteProfit(long id); 
    }
}