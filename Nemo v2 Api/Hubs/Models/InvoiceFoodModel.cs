using Nemo_v2_Data;

namespace Nemo_v2_Api.Hubs.Models
{
    public class InvoiceFoodModel
    {
        private long id;
        private string name;
        private int count;
        private float size;
        private bool confrimed;
        private bool isNew;
        private bool isGift;
        private UserDto user;
        private decimal originalPrice;
        private decimal changedPrice;

        public decimal OriginalPrice
        {
            get => originalPrice;
        }

        public decimal ChangedPrice
        {
            get => changedPrice;
        }

        public long Id
        {
            get => id; set
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

        public UserDto User
        {
            get { return this.user; }
        }
    }
}