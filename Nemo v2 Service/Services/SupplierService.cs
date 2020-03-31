using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class SupplierService:ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;

        public SupplierService(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _supplierRepository.Get();
        }

        public IEnumerable<Supplier> GetSuppliersByRestaurantId(long RestId)
        {
            return _supplierRepository.Query(x => x.RestaurantId == RestId);
        }

        public Supplier GetSupplier(long id)
        {
            return _supplierRepository.GetById(id);
        }

        public Supplier InsertSupplier(Supplier Supplier)
        {
            return _supplierRepository.Insert(Supplier);
        }

        public Supplier UpdateSupplier(Supplier Supplier)
        {
            return _supplierRepository.Update(Supplier);
        }

        public void DeleteSupplier(long id)
        {
            _supplierRepository.Delete(id);
        }
    }
}