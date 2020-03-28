using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IIngredientCategoryService
    {
        IEnumerable<IngredientCategory> Get();  
        IEnumerable<IngredientCategory> GetIngredientCategoryByRestaurantId(long RestId);  
        IngredientCategory GetIngredientCategory(long id);  
        IngredientCategory InsertIngredientCategory(IngredientCategory IngredientCategory);  
        IngredientCategory UpdateIngredientCategory(IngredientCategory IngredientCategory);  
        void DeleteIngredientCategory(long id); 
    }
}