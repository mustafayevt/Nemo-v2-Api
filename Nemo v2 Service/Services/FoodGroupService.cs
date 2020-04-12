using System;
using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class FoodGroupService : IFoodGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FoodGroup> Get()
        {
            return _unitOfWork.FoodGroupRepository.Get();
        }

        public IEnumerable<FoodGroup> GetFoodGroupByRestaurantId(long RestId)
        {
            return _unitOfWork.FoodGroupRepository.Query(x => x.RestaurantId == RestId);
        }

        public FoodGroup GetFoodGroup(long id)
        {
            return _unitOfWork.FoodGroupRepository.GetById(id);
        }

        public FoodGroup InsertFoodGroup(FoodGroup FoodGroup)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.FoodGroupRepository.Insert(FoodGroup);
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

        public FoodGroup UpdateFoodGroup(FoodGroup FoodGroup)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.FoodGroupRepository.Update(FoodGroup);
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

        public void DeleteFoodGroup(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.FoodGroupRepository.Delete(id);
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