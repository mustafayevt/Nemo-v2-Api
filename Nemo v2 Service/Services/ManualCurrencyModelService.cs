using System;
using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class ManualCurrencyModelService : IManualCurrencyModelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManualCurrencyModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<ManualCurrencyModel> Get()
        {
            return _unitOfWork.ManualCurrencyModelRepository.Get();
        }

        public IEnumerable<ManualCurrencyModel> GetManualCurrencyModelsByRestaurantId(long RestId)
        {
            return _unitOfWork.ManualCurrencyModelRepository.Query(x => x.RestaurantId == RestId);
        }

        public ManualCurrencyModel GetManualCurrencyModel(long id)
        {
            return _unitOfWork.ManualCurrencyModelRepository.GetById(id);
        }

        public ManualCurrencyModel InsertManualCurrencyModel(ManualCurrencyModel manualCurrencyModel)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result =  _unitOfWork.ManualCurrencyModelRepository.Insert(manualCurrencyModel);
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

        public ManualCurrencyModel UpdateManualCurrencyModel(ManualCurrencyModel manualCurrencyModel)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result =  _unitOfWork.ManualCurrencyModelRepository.Update(manualCurrencyModel);
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

        public void DeleteManualCurrencyModel(long id)
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