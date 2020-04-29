using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Nemo_v2_Api.Hubs.Models;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.Helper;
using Nemo_v2_Service.Abstraction;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nemo_v2_Api.Hubs
{
    public class POSHub : Hub
    {
        private static List<InvoiceModel> _invoiceModels = new List<InvoiceModel>();

        private readonly IInvoiceService _invoiceService;

        public POSHub(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task BranchConnected(long branchId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, branchId.ToString());
            var invoices = _invoiceModels.Where(x => x.RestaurantId == branchId);
            if(invoices.Any())
            await Clients.Caller.SendAsync("ReciveInvoices", JsonConvert.SerializeObject(invoices));
        }

        public async Task OpenInvoice(string InvoiceModel)
        {
            var newInvoice = JsonConvert.DeserializeObject<InvoiceModel>(InvoiceModel);

            if (_invoiceModels.All(x => x.Id != newInvoice.Id))
            {
                _invoiceModels.Add(newInvoice);
                try
                {
                    await Clients.OthersInGroup(newInvoice.RestaurantId.ToString()).SendAsync("NewInvoice",InvoiceModel);
                }
                catch (Exception e)
                {
                }
            }
        }

        public async Task UpdateInvoice(string InvoiceModel)
        {
            var updatedInvoice = JsonConvert.DeserializeObject<InvoiceModel>(InvoiceModel);

            var currentInvoice = _invoiceModels.FirstOrDefault(x => x.Id == updatedInvoice.Id);
            if (currentInvoice != null)
            {
                currentInvoice = updatedInvoice;
                try
                {
                    await Clients.OthersInGroup(updatedInvoice.RestaurantId.ToString()).SendAsync("UpdateInvoice", InvoiceModel);
                }
                catch (Exception e)
                {
                }
            }
        }
        
        public async Task CloseInvoice(string InvoiceModel)
        {
            var closedInvoice = JsonConvert.DeserializeObject<InvoiceModel>(InvoiceModel);

            var currentInvoice = _invoiceModels.FirstOrDefault(x => x.Id == closedInvoice.Id);
            if (currentInvoice != null)
            {
                _invoiceModels.Remove(currentInvoice);
                try
                {
                    await Clients.OthersInGroup(closedInvoice.RestaurantId.ToString()).SendAsync("CloseInvoice", InvoiceModel);
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}