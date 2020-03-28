using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IIngredientService
    {
        IEnumerable<Ingredient> Get();  
        IEnumerable<Ingredient> GetIngredientByRestaurantId(long RestId);  
        Ingredient GetIngredient(long id);  
        Ingredient InsertIngredient(Ingredient Ingredient);  
        Ingredient UpdateIngredient(Ingredient Ingredient);  
        void DeleteIngredient(long id); 
    }
}