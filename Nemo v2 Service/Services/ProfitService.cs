using System;
using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class ProfitService:IProfitService
    {
        private IUnitOfWork _unitOfWork;

        public ProfitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Profit> Get()
        {
            return _unitOfWork.ProfitRepository.Get();
        }

        public IEnumerable<Profit> GetProfitByRestaurantId(long RestId)
        {
            return _unitOfWork.ProfitRepository.Query(x => x.RestaurantId == RestId);
        }

        public Profit GetProfit(long id)
        {
            return _unitOfWork.ProfitRepository.GetById(id);
        }

        public Profit InsertProfit(Profit Profit)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.ProfitRepository.Insert(Profit);
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

        public Profit UpdateProfit(Profit Profit)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.ProfitRepository.Update(Profit);
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

        public void DeleteProfit(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.ProfitRepository.Delete(id);
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