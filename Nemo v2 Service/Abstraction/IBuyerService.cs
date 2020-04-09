using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IBuyerService
    {
        IEnumerable<Buyer> GetBuyers();
        IEnumerable<Buyer> GetBuyersByRestaurantId(long RestId);
        Buyer GetBuyer(long id);
        Buyer InsertBuyer(Buyer Buyer);
        Buyer UpdateBuyer(Buyer Buyer);
        void DeleteBuyer(long id);
    }
}