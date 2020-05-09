using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class WarehouseTransferInvoiceService : IWarehouseTransferInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IIngredientService _ingredientService;

        public IEnumerable<WarehouseTransferInvoice> Get()
        {
            return _unitOfWork.WarehouseTransferInvoiceRepository.Get();
        }

        public IEnumerable<WarehouseTransferInvoice> GetWarehouseTransferInvoiceByRestaurantId(long RestId)
        {
            return _unitOfWork.WarehouseTransferInvoiceRepository.Query(x => x.RestaurantId == RestId);
        }

        public WarehouseTransferInvoice GetWarehouseTransferInvoice(long id)
        {
            return _unitOfWork.WarehouseTransferInvoiceRepository.Query(x => x.Id == id)
                .First();
        }

        public WarehouseTransferInvoice InsertWarehouseTransferInvoice(
            WarehouseTransferInvoice WarehouseTransferInvoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();

                var warehouseTransferInvoice =
                    _unitOfWork.WarehouseTransferInvoiceRepository.Insert(WarehouseTransferInvoice);
                _ingredientService.DecreaseIngredientQuantity(new List<IngredientWarehouseRel>()
                {
                    new IngredientWarehouseRel()
                    {
                        IngredientId = warehouseTransferInvoice.IngredientId,
                        WarehouseId = warehouseTransferInvoice.AcceptorWarehouseId,
                        Quantity = warehouseTransferInvoice.Quantity
                    }
                });

                _ingredientService.IncreaseIngredientQuantity(new List<IngredientWarehouseRel>
                {
                    new IngredientWarehouseRel()
                    {
                        IngredientId = warehouseTransferInvoice.IngredientId,
                        WarehouseId = warehouseTransferInvoice.RequesterWarehouseId,
                        Quantity = warehouseTransferInvoice.Quantity
                    }
                });
                
                _unitOfWork.Save();
                _unitOfWork.Commit();

                return warehouseTransferInvoice;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public WarehouseTransferInvoice UpdateWarehouseTransferInvoice(
            WarehouseTransferInvoice WarehouseTransferInvoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.WarehouseTransferInvoiceRepository.Update(WarehouseTransferInvoice);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void DeleteWarehouseTransferInvoice(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.WarehouseTransferInvoiceRepository.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}