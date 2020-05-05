using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Api.Hubs.Models
{
    public class InvoiceModel
    {
        public string Id { get; set; }
        public long RestaurantId { get; set; }
        public long SectionId { get; set; }
        public long TableId { get; set; }
        public short PeopleCount { get; set; }

        public List<TableDto> Tables { get; set; }

        public UserDto OpenedUser { get; set; }
        public UserDto ClosedUser { get; set; }
        public decimal Discount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceType InvoiceType { get; set; }

        private ObservableCollection<InvoiceFoodModel> invoiceFoodViewModels = new ObservableCollection<InvoiceFoodModel>();

        public ObservableCollection<InvoiceFoodModel> InvoiceFoodViewModels
        {
            get => invoiceFoodViewModels;
            set
            {
                invoiceFoodViewModels = value;
            }
        }
    }
}