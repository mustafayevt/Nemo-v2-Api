using System;
using System.Collections.Generic;
using System.Linq;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class BuyerService:IBuyerService
    {
        private readonly IRepository<Buyer> _buyerRepository;
        private readonly IRepository<Restaurant> _restaurantRepository;

        public BuyerService(IRepository<Buyer> buyerRepository,
            IRepository<Restaurant> restaurantRepository)
        {
            _buyerRepository = buyerRepository;
            _restaurantRepository = restaurantRepository;
        }

        public IEnumerable<Buyer> GetBuyers()
        {
            return _buyerRepository.Get();
        }

        public IEnumerable<Buyer> GetBuyersByRestaurantId(long RestId)
        {
             return _buyerRepository.Query(x => x.RestBuyerRels.Count(y => y.RestaurantId == RestId) > 0);
            return null;
        }

        public Buyer GetBuyer(long id)
        {
            return _buyerRepository.GetById(id);
        }

        public Buyer InsertBuyer(Buyer Buyer)
        {
            if (Buyer.RestBuyerRels?.Any() ?? false)
            {
                if (Buyer.RestBuyerRels.Any(x => x.Restaurant.Id == 0))
                {
                    throw new NullReferenceException("Restaurant Not Found");
                }

                var restaurantsId = Buyer.RestBuyerRels.Select(x => x.Restaurant.Id);
                var selectedRestaurants = _restaurantRepository.Query(x => restaurantsId.Contains(x.Id));
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
            return _buyerRepository.Insert(Buyer);
        }

        public Buyer UpdateBuyer(Buyer Buyer)
        {
            if (Buyer.RestBuyerRels?.Any() ?? false)
            {
                if (Buyer.RestBuyerRels.Any(x => x.Restaurant.Id == 0))
                {
                    throw new NullReferenceException("Restaurant Not Found");
                }

                var restaurantsId = Buyer.RestBuyerRels.Select(x => x.Restaurant.Id);
                var selectedRestaurants = _restaurantRepository.Query(x => restaurantsId.Contains(x.Id));
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
            return _buyerRepository.Update(Buyer);
        }

        public void DeleteBuyer(long id)
        {
            _buyerRepository.Delete(id);
        }
    }
}