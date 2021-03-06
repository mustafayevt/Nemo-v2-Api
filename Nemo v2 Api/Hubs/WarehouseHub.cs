﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Nemo_v2_Data.Entities;
using Nemo_v2_Data.SignalrModels.WarehouseTransfer;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Service.Abstraction;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nemo_v2_Api.Hubs
{
    public class WarehouseHub:Hub
    {
        // private static List<TransferIngredientModel> _transferIngredientModels = new List<TransferIngredientModel>();
        private readonly IWarehouseService _warehouseService;
        private readonly IRestaurantService _restaurantService;
        private readonly HubTemporaryDataContext _hubTemporaryDataContext;

        public WarehouseHub(IWarehouseService warehouseService, IRestaurantService restaurantService, HubTemporaryDataContext hubTemporaryDataContext)
        {
            _warehouseService = warehouseService;
            _restaurantService = restaurantService;
            _hubTemporaryDataContext = hubTemporaryDataContext;
        }

        public async Task WarehouseConnected(long restaurantId,long warehouseId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, warehouseId.ToString());

            var warehouse = _warehouseService.GetWarehouse(warehouseId);
            var warehouseTransferIngredients =
                _hubTemporaryDataContext.TransferIngredientModels.Select(x =>
                    JsonConvert.DeserializeObject<TransferIngredientModel>(x.JsonData)).Where(x=>
                    warehouse.IngredientWarehouseRels.Count(y=>y.IngredientId == x.IngredientId)>0 &&
                    x.RequestedWareHouseId !=warehouseId);
            

            if (warehouseTransferIngredients.Any())
            {
                await Clients.Caller.SendAsync("ReceiveTransferRequests", JsonConvert.SerializeObject(warehouseTransferIngredients));
            }
        }

        public async Task SendTransferRequest(string transferIngredientModel,long restaurantId)
        {
            var TransferIngredientModel =
                JsonConvert.DeserializeObject <List<TransferIngredientModel>>(transferIngredientModel);
            
            TransferIngredientModel.ForEach(x => { x.Id = Guid.NewGuid().ToString(); });

            foreach (var ingredientModel in TransferIngredientModel)
            {
                _hubTemporaryDataContext.TransferIngredientModels.Add(
                    new WarehouseTransferDbModel(ingredientModel.Id,JsonConvert.SerializeObject(ingredientModel)));
            }
            await _hubTemporaryDataContext.SaveChangesAsync();
            
            
            var restaurantWarehouses = _warehouseService.GetWarehousesByRestaurantId(restaurantId);
            foreach (var model in TransferIngredientModel)
            {
                foreach (var warehouse in restaurantWarehouses.Where(x=>x.IngredientWarehouseRels
                                                                            .Count(y=>y.IngredientId == model.IngredientId)>0))
                {
                    Clients.OthersInGroup(warehouse.Id.ToString()).SendAsync("NewTransfer", JsonConvert.SerializeObject(model));
                }
            }
        }

        public async Task TransferAccepted(string transferIngredientModel)
        {
            var TransferIngredientModel =
                JsonConvert.DeserializeObject <List<TransferIngredientModel>>(transferIngredientModel);

            TransferIngredientModel.ForEach(x => 
                _hubTemporaryDataContext.TransferIngredientModels.Remove(
                    _hubTemporaryDataContext.TransferIngredientModels.First(y=>y.TransferId == x.Id)));
            await _hubTemporaryDataContext.SaveChangesAsync();
            
            //todo:database insert

            await Clients.Group(TransferIngredientModel[0].RequestedWareHouseId.ToString())
                .SendAsync("TransferAccepted", transferIngredientModel);
        }
        
        public async Task TransferRejected(string transferIngredientModel)
        {
            var TransferIngredientModel =
                JsonConvert.DeserializeObject <List<TransferIngredientModel>>(transferIngredientModel);
            
            
            await Clients.Group(TransferIngredientModel[0].RequestedWareHouseId.ToString())
                .SendAsync("TransferRejected", transferIngredientModel);
        }
    }
}