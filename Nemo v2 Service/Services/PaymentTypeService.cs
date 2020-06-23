using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PaymentType> Get()
        {
            return _unitOfWork.PaymentTypeRepository.Get();
        }

        public IEnumerable<PaymentType> GetPaymentTypeByRestaurantId(long restId)
        {
            return _unitOfWork.PaymentTypeRepository.Query(x => x.RestaurantId == restId);
        }

        public PaymentType GetPaymentType(long id)
        {
            return _unitOfWork.PaymentTypeRepository.GetById(id);
        }

        public PaymentType InsertPaymentType(PaymentType paymentType)
        {
            var result = _unitOfWork.PaymentTypeRepository.Insert(paymentType);
            _unitOfWork.Save();
            return result;
        }

        public PaymentType UpdatePaymentType(PaymentType paymentType)
        {
            var result =  _unitOfWork.PaymentTypeRepository.Update(paymentType);
            _unitOfWork.Save();
            return result;
        }

        public void DeletePaymentType(long id)
        {
            _unitOfWork.PaymentTypeRepository.Delete(id);
            _unitOfWork.Save();
            _unitOfWork.Commit();
        }
    }
}