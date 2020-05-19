using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IManualCurrencyModelService
    {
        IEnumerable<ManualCurrencyModel> Get();  
        IEnumerable<ManualCurrencyModel> GetManualCurrencyModelsByRestaurantId(long RestId);  
        ManualCurrencyModel GetManualCurrencyModel(long id);  
        ManualCurrencyModel InsertManualCurrencyModel(ManualCurrencyModel manualCurrencyModel);  
        ManualCurrencyModel UpdateManualCurrencyModel(ManualCurrencyModel manualCurrencyModel);  
        void DeleteManualCurrencyModel(long id);  
    }
}