using System;
using System.Collections.Generic;
using System.Linq;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuyerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Buyer> GetBuyers()
        {
            return _unitOfWork.BuyerRepository.Get();
        }

        public IEnumerable<Buyer> GetBuyersByRestaurantId(long RestId)
        {
            return _unitOfWork.BuyerRepository.Query(x => x.RestBuyerRels.Count(y => y.RestaurantId == RestId) > 0);
        }

        public Buyer GetBuyer(long id)
        {
            return _unitOfWork.BuyerRepository.GetById(id);
        }

        public Buyer InsertBuyer(Buyer Buyer)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Buyer.RestBuyerRels?.Any() ?? false)
                {
                    if (Buyer.RestBuyerRels.Any(x => x.Restaurant.Id == 0))
                    {
                        throw new NullReferenceException("Restaurant Not Found");
                    }

                    var restaurantsId = Buyer.RestBuyerRels.Select(x => x.Restaurant.Id);
                    var selectedRestaurants = _unitOfWork.RestaurantRepository.Query(x => restaurantsId.Contains(x.Id));
                    if (selectedRestaurants.Count() != restaurantsId.Count())
                        throw new NullReferenceException("Restaurant Not Found");

                    Buyer.RestBuyerRels = selectedRestaurants.Select(x =>
                        new RestBuyerRel()
                        {
                            RestaurantId = x.Id,
                            BuyerId = Buyer.Id
                        }
                    ).ToList();
                }

                var result = _unitOfWork.BuyerRepository.Insert(Buyer);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
        }

        public Buyer UpdateBuyer(Buyer Buyer)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (Buyer.RestBuyerRels?.Any() ?? false)
                {
                    if (Buyer.RestBuyerRels.Any(x => x.Restaurant.Id == 0))
                    {
                        throw new NullReferenceException("Restaurant Not Found");
                    }

                    var restaurantsId = Buyer.RestBuyerRels.Select(x => x.Restaurant.Id);
                    var selectedRestaurants = _unitOfWork.RestaurantRepository.Query(x => restaurantsId.Contains(x.Id));
                    if (selectedRestaurants.Count() != restaurantsId.Count())
                        throw new NullReferenceException("Restaurant Not Found");

                    Buyer.RestBuyerRels = selectedRestaurants.Select(x =>
                        new RestBuyerRel()
                        {
                            RestaurantId = x.Id,
                            BuyerId = Buyer.Id
                        }
                    ).ToList();
                }

                var result = _unitOfWork.BuyerRepository.Update(Buyer);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
        }

        public void DeleteBuyer(long id)
        {
            try
            {
            _unitOfWork.BuyerRepository.Delete(id);
            _unitOfWork.Save();
            _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
        }
    }
}