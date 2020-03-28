using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IWarehouseService
    {
        IEnumerable<Warehouse> GetWarehouses();
        IEnumerable<Warehouse> GetWarehousesByRestaurantId(long RestId);
        Warehouse GetWarehouse(long id);
        Warehouse InsertWarehouse(Warehouse Warehouse,IEnumerable<long> restaurantIds);
        Warehouse UpdateWarehouse(Warehouse Warehouse,IEnumerable<long> restaurantIds);
        void DeleteWarehouse(long id);
    }
}