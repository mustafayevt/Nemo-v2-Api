using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<Restaurant> _restaurantRepository;

        public WarehouseService(IRepository<Warehouse> warehouseRepository,
            IRepository<Restaurant> repositoryRepository)
        {
            _warehouseRepository = warehouseRepository;
            _restaurantRepository = repositoryRepository;
        }

        public IEnumerable<Warehouse> GetWarehouses()
        {
            return _warehouseRepository.Get();
        }

        public IEnumerable<Warehouse> GetWarehousesByRestaurantId(long RestId)
        {
            return _warehouseRepository.Query(x => x.RestWareRels.Count(y => y.RestaurantId == RestId) > 0);
        }

        public Warehouse GetWarehouse(long id)
        {
            return _warehouseRepository.GetById(id);
        }

        public Warehouse InsertWarehouse(Warehouse Warehouse)
        {
            if (Warehouse.RestWareRels?.Any() ?? false)
            {
                if (Warehouse.RestWareRels.Any(x => x.Restaurant.Id == 0))
                {
                    throw new NullReferenceException("Restaurant Not Found");
                }

                var restaurantsId = Warehouse.RestWareRels.Select(x => x.Restaurant.Id);
                var selectedRestaurants = _restaurantRepository.Query(x => restaurantsId.Contains(x.Id));
                if (selectedRestaurants.Count() != restaurantsId.Count())
                    throw new NullReferenceException("Restaurant Not Found");

                Warehouse.RestWareRels = selectedRestaurants.Select(x =>
                    new RestWareRel()
                    {
                        RestaurantId = x.Id,
                        WarehouseId = Warehouse.Id
                    }
                ).ToList();
            }

            return _warehouseRepository.Insert(Warehouse);
        }

        public Warehouse UpdateWarehouse(Warehouse Warehouse)
        {
            if (Warehouse.RestWareRels?.Any() ?? false)
            {
                if (Warehouse.RestWareRels.Any(x => x.Restaurant.Id == 0))
                {
                    throw new NullReferenceException("Restaurant Not Found");
                }

                var restaurantsId = Warehouse.RestWareRels.Select(x => x.Restaurant.Id);
                var selectedRestaurants = _restaurantRepository.Query(x => restaurantsId.Contains(x.Id));
                if (selectedRestaurants.Count() != restaurantsId.Count())
                    throw new NullReferenceException("Restaurant Not Found");

                Warehouse.RestWareRels = selectedRestaurants.Select(x =>
                    new RestWareRel()
                    {
                        RestaurantId = x.Id,
                        WarehouseId = Warehouse.Id
                    }
                ).ToList();
            }

            return _warehouseRepository.Update(Warehouse);
        }

        public void DeleteWarehouse(long id)
        {
            _warehouseRepository.Delete(id);
        }
    }
}