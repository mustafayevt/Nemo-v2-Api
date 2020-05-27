using Nemo_v2_Data;

namespace Nemo_v2_Api.Hubs.Models
{
    public class InvoiceFoodModel
    {
        private string localId;
        private long id;
        private string name;
        private int count;
        private float size;
        private bool confrimed;
        private bool isNew;
        private bool isGift;
        private bool isNonPayable;
        private UserDto user;
        private decimal originalPrice;
        private decimal changedPrice;

        private TableDto ownerTable;

        public TableDto OwnerTable
        {
            get
            {
                return ownerTable;
            }
            set
            {
                ownerTable = value;
            }
        }
        
        public string LocalId 
        {
            get
            {
                return localId;
            }
            set
            {
                localId = value;
            }
        }

        public decimal OriginalPrice
        {
            get => originalPrice;
            set
            {
                originalPrice = value;
            }
        }

        public decimal ChangedPrice
        {
            get => changedPrice;
            set
            {
                changedPrice = value;
            }
        }

        public long Id
        {
            get => id; 
            set
            {
                id = value;
            }
        }
        public string Name
        {
            get => name; set
            {
                name = value;
            }
        }
        public int Count
        {
            get => count; set
            {
                count = value;
            }
        }
        public float Size
        {
            get => size;
            set
            {
                size = value;
            }
        }
        public bool Confrimed
        {
            get => confrimed; set
            {
                confrimed = value;
            }
        }
        public bool IsNew
        {
            get => isNew; set
            {
                isNew = value;
            }
        }
        public bool IsGift
        {
            get => isGift; set
            {
                isGift = value;
            }
        }
        public bool IsNonPayable
        {
            get => isNonPayable;
            set
            {
                isNonPayable = value;
            }
        }
        public string IsGiftOrNewOrConfrimed
        {
            get
            {
                return IsNonPayable ? "Non-Payable" : IsGift ? "Gift" : IsNew ? "New" : "Confrimed";
            }
        }

        public UserDto User
        {
            get { return this.user; }
            set
            {
                user = value;
            }
        }
    }
}