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
        private readonly IUnitOfWork _unitOfWork;

        public WarehouseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Warehouse> GetWarehouses()
        {
            return _unitOfWork.WarehouseRepository.Get();
        }

        public IEnumerable<Warehouse> GetWarehousesByRestaurantId(long RestId)
        {
            return _unitOfWork.WarehouseRepository.Query(x => x.RestWareRels.Count(y => y.RestaurantId == RestId) > 0)
                .Include(y=>y.IngredientWarehouseRels);
        }

        public Warehouse GetWarehouse(long id)
        {
            return _unitOfWork.WarehouseRepository.Query(x => x.Id == id)
                .Include(y => y.IngredientWarehouseRels)
                .First();
        }

        public Warehouse InsertWarehouse(Warehouse Warehouse)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Warehouse.RestWareRels?.Any() ?? false)
                {
                    if (Warehouse.RestWareRels.Any(x => x.Restaurant.Id == 0))
                    {
                        throw new NullReferenceException("Restaurant Not Found");
                    }

                    var restaurantsId = Warehouse.RestWareRels.Select(x => x.Restaurant.Id);
                    var selectedRestaurants = _unitOfWork.RestaurantRepository.Query(x => restaurantsId.Contains(x.Id));
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

                var result = _unitOfWork.WarehouseRepository.Insert(Warehouse);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public Warehouse UpdateWarehouse(Warehouse Warehouse)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Warehouse.RestWareRels?.Any() ?? false)
                {
                    if (Warehouse.RestWareRels.Any(x => x.Restaurant.Id == 0))
                    {
                        throw new NullReferenceException("Restaurant Not Found");
                    }

                    var restaurantsId = Warehouse.RestWareRels.Select(x => x.Restaurant.Id);
                    var selectedRestaurants = _unitOfWork.RestaurantRepository.Query(x => restaurantsId.Contains(x.Id));
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

                var result = _unitOfWork.WarehouseRepository.Update(Warehouse);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void DeleteWarehouse(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.WarehouseRepository.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}