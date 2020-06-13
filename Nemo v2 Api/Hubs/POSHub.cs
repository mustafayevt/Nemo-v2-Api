﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
            var invoices = _hubTemporaryDataContext.InvoiceModels.Where(x => x.BranchId == branchId)
                .Select(x => JsonConvert.DeserializeObject<InvoiceModel>(x.JsonData));
            if (invoices.Any())
                await Clients.Caller.SendAsync("ReciveInvoices", JsonConvert.SerializeObject(invoices));
        }

        public async Task OpenInvoice(string InvoiceModel)
        {
            var newInvoice = JsonConvert.DeserializeObject<InvoiceModel>(InvoiceModel);

            if (_hubTemporaryDataContext.InvoiceModels.AsQueryable().All(x => x.InvoiceId != newInvoice.Id))
            {
                try
                {
                    _hubTemporaryDataContext.InvoiceModels.Add(new InvoiceDbMoel(newInvoice.Id, newInvoice.RestaurantId,
                        InvoiceModel));
                    await _hubTemporaryDataContext.SaveChangesAsync();
                    await Clients.OthersInGroup(newInvoice.RestaurantId.ToString())
                        .SendAsync("NewInvoice", InvoiceModel);
                }
                catch (Exception e)
                {
                }
            }
        }

        public async Task UpdateInvoice(string InvoiceModel)
        {
            var updatedInvoice = JsonConvert.DeserializeObject<InvoiceModel>(InvoiceModel);

            var currentInvoice = _hubTemporaryDataContext.InvoiceModels.AsQueryable()
                .FirstOrDefault(x => x.InvoiceId == updatedInvoice.Id);
            if (currentInvoice != null)
            {
                currentInvoice.JsonData = InvoiceModel;
                _hubTemporaryDataContext.SaveChanges();
                try
                {
                    await Clients.OthersInGroup(updatedInvoice.RestaurantId.ToString())
                        .SendAsync("UpdateInvoice", InvoiceModel);
                }
                catch (Exception e)
                {
                }
            }
        }

        public async Task RemoveInvoice(string invoiceId)
        {
            var removeInvoice = _hubTemporaryDataContext.InvoiceModels.AsQueryable()
                .FirstOrDefault(x => x.InvoiceId == invoiceId);
            if (removeInvoice != null)
            {
                _hubTemporaryDataContext.InvoiceModels.Remove(removeInvoice);
                _hubTemporaryDataContext.SaveChanges();
            }
        }

        public async Task CloseInvoice(string invoiceModel,bool decreaseIngredients)
        {
            var closedInvoice = JsonConvert.DeserializeObject<InvoiceModel>(invoiceModel);

            try
            {
                var invoice = new Invoice()
                {
                    Amount = closedInvoice.Amount,
                    Discount = closedInvoice.Discount,
                    CardPayment = closedInvoice.CardPayment,
                    CashPayment = closedInvoice.CashPayment,
                    RestaurantId = closedInvoice.RestaurantId,
                    SectionId = closedInvoice.Tables.First().SectionId,
                    ClosedUserId = closedInvoice.ClosedUser.Id,
                    OpenedUserId = closedInvoice.OpenedUser.Id,
                    PeopleCount = closedInvoice.PeopleCount,
                    ServiceCharge = closedInvoice.ServiceCharge,
                    TotalAmount = closedInvoice.TotalAmount,
                    InvoiceTableRels = closedInvoice.Tables.Select(y => new InvoiceTableRel {TableId = y.Id}).ToList()
                };
                invoice.Foods = new List<FoodInvoiceRel>();
                foreach (var invoiceFoodModel in closedInvoice.InvoiceFoodViewModels.GroupBy(y=>y.Id))
                {
                    invoice.Foods.Add(new FoodInvoiceRel()
                    {
                        FoodId = invoiceFoodModel.Key,
                        FoodInvoiceProperties = closedInvoice.InvoiceFoodViewModels
                            .Where(x=>x.Id == invoiceFoodModel.Key)
                            .Select(y=>new FoodInvoiceProperties()
                            {
                                Portion = y.Size,
                                Count = y.Count,
                                ChangedPrice = y.ChangedPrice,
                                OriginalPrice = y.OriginalPrice,
                                TableId = y.OwnerTable.Id
                            }).ToList()
                    });
                }
                
                _invoiceService.InsertInvoice(invoice,decreaseIngredients);

                var currentInvoice = _hubTemporaryDataContext.InvoiceModels.AsQueryable()
                    .FirstOrDefault(x => x.InvoiceId == closedInvoice.Id);
                if (currentInvoice != null)
                {
                    try
                    {
                        _hubTemporaryDataContext.InvoiceModels.Remove(currentInvoice);
                        await _hubTemporaryDataContext.SaveChangesAsync();
                        await Clients.Group(closedInvoice.RestaurantId.ToString())
                            .SendAsync("CloseInvoice", invoiceModel);
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