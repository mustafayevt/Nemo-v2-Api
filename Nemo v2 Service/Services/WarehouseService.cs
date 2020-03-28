using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class WarehouseService:IWarehouseService
    {
        private IRepository<Warehouse> _warehouseRepository;
        private IRepository<Restaurant> _restaurantRepository;
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
            return _warehouseRepository.Query(x => x.RestWareRels.Count(y => y.RestaurantId==RestId)>0);
        }

        public Warehouse GetWarehouse(long id)
        {
            return _warehouseRepository.GetById(id);
        }

        public Warehouse InsertWarehouse(Warehouse Warehouse,IEnumerable<long> restaurantIds)
        {
            if (restaurantIds?.Any() ?? false)
            {
                var restaurants = _restaurantRepository.Query(x => restaurantIds.Contains(x.Id)).ToList();
                if(restaurants.Count() != restaurantIds.Count()) throw  new ArgumentException("Restaurant Not Found");

                var RestWareRels = new List<RestWareRel>();
                restaurants.ForEach(x => RestWareRels.Add(new RestWareRel()
                {
                    WarehouseId = Warehouse.Id,
                    RestaurantId = x.Id
                }));
                Warehouse.RestWareRels = RestWareRels;
            }
            return _warehouseRepository.Insert(Warehouse);
        }

        public Warehouse UpdateWarehouse(Warehouse Warehouse,IEnumerable<long> restaurantIds)
        {
            if (_warehouseRepository.Query(x => x.Id == Warehouse.Id).AsNoTracking() == null)
                throw new NullReferenceException("Warehouse Not Found");
            if (restaurantIds?.Any() ?? false)
            {
                var restaurants = _restaurantRepository.Query(x => restaurantIds.Contains(x.Id)).ToList();
                if(restaurants.Count() != restaurantIds.Count()) throw  new ArgumentException("Restaurant Not Found");

                var RestWareRels = new List<RestWareRel>();
                restaurants.ForEach(x => RestWareRels.Add(new RestWareRel()
                {
                    WarehouseId = Warehouse.Id,
                    RestaurantId = x.Id
                }));
                Warehouse.RestWareRels = RestWareRels;
            }
            return _warehouseRepository.Update(Warehouse);
        }

        public void DeleteWarehouse(long id)
        {
            _warehouseRepository.Delete(id);
        }
    }
}