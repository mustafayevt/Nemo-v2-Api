namespace Nemo_v2_Data.Entities
{
    public class ManualCurrencyModel:BaseEntity
    {
        public long RestaurantId { get; set; }
        public bool MainCurrency { get; set; }
        public string Currency { get; set; }
        public decimal Value { get; set; }
    }
}