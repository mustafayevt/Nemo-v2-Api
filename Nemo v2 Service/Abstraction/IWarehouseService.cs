using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IWarehouseService
    {
        IEnumerable<Warehouse> GetWarehouses();
        IEnumerable<Warehouse> GetWarehousesByRestaurantId(long RestId);
        Warehouse GetWarehouse(long id);
        Warehouse InsertWarehouse(Warehouse Warehouse);
        Warehouse UpdateWarehouse(Warehouse Warehouse);
        void DeleteWarehouse(long id);
    }
}