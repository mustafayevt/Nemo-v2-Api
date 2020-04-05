using System;
using System.Collections.Generic;
using System.Linq;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class SupplierService:ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<Restaurant> _restaurantRepository;

        public SupplierService(IRepository<Supplier> supplierRepository,
            IRepository<Restaurant> restaurantRepository)
        {
            _supplierRepository = supplierRepository;
            _restaurantRepository = restaurantRepository;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _supplierRepository.Get();
        }

        public IEnumerable<Supplier> GetSuppliersByRestaurantId(long RestId)
        {
             return _supplierRepository.Query(x => x.RestSupplierRels.Count(y => y.RestaurantId == RestId) > 0);
            return null;
        }

        public Supplier GetSupplier(long id)
        {
            return _supplierRepository.GetById(id);
        }

        public Supplier InsertSupplier(Supplier Supplier)
        {
            if (Supplier.RestSupplierRels?.Any() ?? false)
            {
                if (Supplier.RestSupplierRels.Any(x => x.Restaurant.Id == 0))
                {
                    throw new NullReferenceException("Restaurant Not Found");
                }

                var restaurantsId = Supplier.RestSupplierRels.Select(x => x.Restaurant.Id);
                var selectedRestaurants = _restaurantRepository.Query(x => restaurantsId.Contains(x.Id));
                if (selectedRestaurants.Count() != restaurantsId.Count())
                    throw new NullReferenceException("Restaurant Not Found");

                Supplier.RestSupplierRels = selectedRestaurants.Select(x =>
                    new RestSupplierRel()
                    {
                        RestaurantId = x.Id,
                        SupplierId = Supplier.Id
                    }
                ).ToList();
            }
            return _supplierRepository.Insert(Supplier);
        }

        public Supplier UpdateSupplier(Supplier Supplier)
        {
            if (Supplier.RestSupplierRels?.Any() ?? false)
            {
                if (Supplier.RestSupplierRels.Any(x => x.Restaurant.Id == 0))
                {
                    throw new NullReferenceException("Restaurant Not Found");
                }

                var restaurantsId = Supplier.RestSupplierRels.Select(x => x.Restaurant.Id);
                var selectedRestaurants = _restaurantRepository.Query(x => restaurantsId.Contains(x.Id));
                if (selectedRestaurants.Count() != restaurantsId.Count())
                    throw new NullReferenceException("Restaurant Not Found");

                Supplier.RestSupplierRels = selectedRestaurants.Select(x =>
                    new RestSupplierRel()
                    {
                        RestaurantId = x.Id,
                        SupplierId = Supplier.Id
                    }
                ).ToList();
            }
            return _supplierRepository.Update(Supplier);
        }

        public void DeleteSupplier(long id)
        {
            _supplierRepository.Delete(id);
        }
    }
}