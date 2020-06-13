using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data
{
    public class WarehouseTransferInvoiceDto
    {
        public string Id { get; set; }

        //newAdded
        public long RestaurantId { get; set; }
        public decimal PriceForEach { get; set; }
        public bool IsPayed { get; set; }
        
        //newAdded end
        
        public string TransferId { get; set; }
        public long IngredientId { get; set; }
        public string IngredientName { get; set; }
        public decimal Quantity { get; set; }
        public string RequestTime { get; set; }
        public string AcceptedTime { get; set; }
        public long RequestedWareHouseId { get; set; }
        public string RequestedWareHouseName { get; set; }
        public long AcceptedWareHouseId { get; set; }
        public string AcceptedWareHouseName { get; set; }

        public long RequestedByUserId { get; set; }
        public string RequestedUserName { get; set; }

        public long AcceptedByUserId { get; set; }
        public string AcceptedUserName { get; set; }
        public bool IsAccepted { get; set; }
        public Unit Unit { get; set; }
    }
}