namespace Nemo_v2_Data.SignalrModels.WarehouseTransfer
{
    public class WarehouseTransferDbModel
    {
        public WarehouseTransferDbModel(string transferId, string jsonData)
        {
            TransferId = transferId;
            JsonData = jsonData;
        }

        public int Id { get; set; }
        public string TransferId { get; set; }
        public string JsonData { get; set; }
    }
}