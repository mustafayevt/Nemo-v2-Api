using System;
using System.Collections.Generic;
using System.Linq;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _unitOfWork.SupplierRepository.Get();
        }

        public IEnumerable<Supplier> GetSuppliersByRestaurantId(long RestId)
        {
            return _unitOfWork.SupplierRepository.Query(
                x => x.RestSupplierRels.Count(y => y.RestaurantId == RestId) > 0);
        }

        public Supplier GetSupplier(long id)
        {
            return _unitOfWork.SupplierRepository.GetById(id);
        }

        public Supplier InsertSupplier(Supplier Supplier)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Supplier.RestSupplierRels?.Any() ?? false)
                {
                    if (Supplier.RestSupplierRels.Any(x => x.Restaurant.Id == 0))
                    {
                        throw new NullReferenceException("Restaurant Not Found");
                    }

                    var restaurantsId = Supplier.RestSupplierRels.Select(x => x.Restaurant.Id);
                    var selectedRestaurants = _unitOfWork.RestaurantRepository.Query(x => restaurantsId.Contains(x.Id));
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

                var result = _unitOfWork.SupplierRepository.Insert(Supplier);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public Supplier UpdateSupplier(Supplier Supplier)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Supplier.RestSupplierRels?.Any() ?? false)
                {
                    if (Supplier.RestSupplierRels.Any(x => x.Restaurant.Id == 0))
                    {
                        throw new NullReferenceException("Restaurant Not Found");
                    }

                    var restaurantsId = Supplier.RestSupplierRels.Select(x => x.Restaurant.Id);
                    var selectedRestaurants = _unitOfWork.RestaurantRepository.Query(x => restaurantsId.Contains(x.Id));
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

                var result = _unitOfWork.SupplierRepository.Update(Supplier);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public void DeleteSupplier(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.SupplierRepository.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }
    }
}