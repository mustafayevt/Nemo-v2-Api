using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IPaymentTypeService
    {
        IEnumerable<PaymentType> Get();  
        IEnumerable<PaymentType> GetPaymentTypeByRestaurantId(long restId);  
        PaymentType GetPaymentType(long id);  
        PaymentType InsertPaymentType(PaymentType paymentType);  
        PaymentType UpdatePaymentType(PaymentType paymentType);  
        void DeletePaymentType(long id); 
    }
}