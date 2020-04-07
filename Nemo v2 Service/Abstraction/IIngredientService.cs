using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IIngredientService
    {
        IEnumerable<Ingredient> Get();  
        IEnumerable<Ingredient> GetIngredientByRestaurantId(long RestId);  
        IEnumerable<Ingredient> GetIngredientByWarehouseId(long WarehouseId);  
        Ingredient GetIngredient(long id);  
        Ingredient InsertIngredient(Ingredient Ingredient);  
        Ingredient UpdateIngredient(Ingredient Ingredient);
        IEnumerable<Ingredient> IncreaseCurrentQuantity(IEnumerable<IngredientsInsert> ingredientsInserts);
        decimal CalculateAveragePrice(long IngredientId,long WarehouseId);
        void DeleteIngredient(long id); 
    }
}