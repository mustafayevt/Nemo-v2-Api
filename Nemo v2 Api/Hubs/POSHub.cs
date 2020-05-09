﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Nemo_v2_Api.Hubs.Models;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Helper;
using Nemo_v2_Service.Abstraction;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nemo_v2_Api.Hubs
{
    public class POSHub : Hub
    {
        // private static List<InvoiceModel> _invoiceModels = new List<InvoiceModel>();

        private readonly IInvoiceService _invoiceService;
        private readonly HubTemporaryDataContext _hubTemporaryDataContext;

        public POSHub(IInvoiceService invoiceService, HubTemporaryDataContext hubTemporaryDataContext)
        {
            _invoiceService = invoiceService;
            _hubTemporaryDataContext = hubTemporaryDataContext;
        }

        public async Task BranchConnected(long branchId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, branchId.ToString());
            var invoices = _hubTemporaryDataContext.InvoiceModels.Where(x => x.RestaurantId == branchId);
            if(invoices.Any())
            await Clients.Caller.SendAsync("ReciveInvoices", JsonConvert.SerializeObject(invoices));
        }

        public async Task OpenInvoice(string InvoiceModel)
        {
            var newInvoice = JsonConvert.DeserializeObject<InvoiceModel>(InvoiceModel);

            if (_hubTemporaryDataContext.InvoiceModels.AsQueryable().All(x => x.Id != newInvoice.Id))
            {
                _hubTemporaryDataContext.InvoiceModels.Add(newInvoice);
                _hubTemporaryDataContext.SaveChangesAsync();
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

            var currentInvoice = _hubTemporaryDataContext.InvoiceModels.AsQueryable().FirstOrDefault(x => x.Id == updatedInvoice.Id);
            if (currentInvoice != null)
            {
                currentInvoice = updatedInvoice;
                _hubTemporaryDataContext.SaveChanges();
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

            try
            {
                _invoiceService.InsertInvoice(new Invoice()
                {
                    Amount = closedInvoice.Amount,
                    Discount = closedInvoice.Discount,
                    RestaurantId = closedInvoice.RestaurantId,
                    SectionId = closedInvoice.SectionId,
                    ClosedUserId = closedInvoice.ClosedUser.Id,
                    OpenedUserId = closedInvoice.OpenedUser.Id,
                    InvoiceType = closedInvoice.InvoiceType,
                    PeopleCount = closedInvoice.PeopleCount,
                    ServiceCharge = closedInvoice.ServiceCharge,
                    TotalAmount = closedInvoice.TotalAmount,
                    Foods =
                        closedInvoice.InvoiceFoodViewModels.Select(y => new FoodInvoiceRel {FoodId = y.Id}).ToList(),
                    InvoiceTableRels = closedInvoice.Tables.Select(y => new InvoiceTableRel {TableId = y.Id}).ToList()
                });
                
                var currentInvoice = _hubTemporaryDataContext.InvoiceModels.FirstOrDefault(x => x.Id == closedInvoice.Id);
                if (currentInvoice != null)
                {
                    _hubTemporaryDataContext.InvoiceModels.Remove(currentInvoice);
                    _hubTemporaryDataContext.SaveChanges();
                    try
                    {
                        await Clients.OthersInGroup(closedInvoice.RestaurantId.ToString()).SendAsync("CloseInvoice", InvoiceModel);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}