using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class RestaurantService:IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Restaurant> Get()
        {
            return _unitOfWork.RestaurantRepository.Get();
        }

        public IEnumerable<Restaurant> GetBranches(long RestId)
        {
            return _unitOfWork.RestaurantRepository.Query(x => x.BranchId == RestId)
                .Include(x=>x.Branches)
                .Include(x=>x.Sections)
                .Include(x=>x.Tables);
        }

        public Restaurant GetRestaurant(long id)
        {
            return _unitOfWork.RestaurantRepository.Query(x => x.Id == id)
                .Include(x => x.Branches).First();
        }

        public Restaurant GetParentByBranchId(long id)
        {
            return _unitOfWork.RestaurantRepository.Query(x => x.Id == GetRestaurant(id).BranchId).First();
        }

        public Restaurant InsertRestaurant(Restaurant restaurant)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.RestaurantRepository.Insert(restaurant);
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

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.RestaurantRepository.Insert(restaurant);
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

        public void DeleteRestaurant(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.RestaurantRepository.Delete(id);
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