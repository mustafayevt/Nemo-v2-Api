using System;
using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class IngredientCategoryService:IIngredientCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<IngredientCategory> Get()
        {
           return _unitOfWork.IngredientCategoryRepository.Get();
        }

        public IEnumerable<IngredientCategory> GetIngredientCategoryByRestaurantId(long RestId)
        {
            return _unitOfWork.IngredientCategoryRepository.Query(x => x.RestaurantId == RestId);
        }

        public IngredientCategory GetIngredientCategory(long id)
        {
            return _unitOfWork.IngredientCategoryRepository.GetById(id);
        }

        public IngredientCategory InsertIngredientCategory(IngredientCategory IngredientCategory)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result =  _unitOfWork.IngredientCategoryRepository.Insert(IngredientCategory);
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

        public IngredientCategory UpdateIngredientCategory(IngredientCategory IngredientCategory)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result =  _unitOfWork.IngredientCategoryRepository.Update(IngredientCategory);
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

        public void DeleteIngredientCategory(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.IngredientCategoryRepository.Delete(id);
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