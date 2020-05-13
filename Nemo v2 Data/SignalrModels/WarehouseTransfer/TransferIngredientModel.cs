using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.SignalrModels.WarehouseTransfer
{
    public class TransferIngredientModel
    {
        public string Id { get; set; }
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