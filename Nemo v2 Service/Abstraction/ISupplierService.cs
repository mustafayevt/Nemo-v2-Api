using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetSuppliers();
        IEnumerable<Supplier> GetSuppliersByRestaurantId(long RestId);
        Supplier GetSupplier(long id);
        Supplier InsertSupplier(Supplier Supplier);
        Supplier UpdateSupplier(Supplier Supplier);
        void DeleteSupplier(long id);
    }
}