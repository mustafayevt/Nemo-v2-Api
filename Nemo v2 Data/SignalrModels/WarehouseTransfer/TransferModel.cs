using System.Collections.ObjectModel;

namespace Nemo_v2_Data.SignalrModels.WarehouseTransfer
{
    public class TransferModel
    {
        public long TransferId { get; set; }
        public bool IsAccepted { get; set; }
        public long RequestedWareHouseId { get; set; }
        public string RequestedWareHouseName { get; set; }
        public long RequestedUserId { get; set; }
        public string RequestedUserName { get; set; }
        public string RequestTime { get; set; }
        public long AcceptedWareHouseId { get; set; }
        public string AcceptedWareHouseName { get; set; }
        public long AcceptedUserId { get; set; }
        public string AcceptedUserName { get; set; }
        public string AcceptedTime { get; set; }
        private ObservableCollection<TransferIngredientModel> transferIngredients = new ObservableCollection<TransferIngredientModel>();

        public ObservableCollection<TransferIngredientModel> TransferIngredients
        {
            get 
            {
                return transferIngredients; 
            }
            set
            {
                transferIngredients = value;
            }
        }
    }
}