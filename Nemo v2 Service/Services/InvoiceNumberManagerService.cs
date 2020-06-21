using System;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class InvoiceNumberManagerService : IInvoiceNumberManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceNumberManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewInvoiceNumber(long restId)
        {
            using (var transaction = _unitOfWork.CreateTransaction())
            {
                try
                {
                    var restaurant = _unitOfWork.RestaurantRepository.GetById(restId);
                    var invoiceNumber = $"I{++restaurant.LastInvoiceNumber:0000000000}";
                    _unitOfWork.Save();
                    transaction.Commit();
                    return invoiceNumber;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public string GetNewWarehouseExportNumber(long restId)
        {
            using (var transaction = _unitOfWork.CreateTransaction())
            {
                try
                {
                    var restaurant = _unitOfWork.RestaurantRepository.GetById(restId);
                    var invoiceNumber = $"WE{++restaurant.LastWarehouseExportInvoiceNumber:0000000000}";
                    _unitOfWork.Save();
                    transaction.Commit();
                    return invoiceNumber;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public string GetNewWarehouseInsertNumber(long restId)
        {
            using (var transaction = _unitOfWork.CreateTransaction())
            {
                try
                {
                    var restaurant = _unitOfWork.RestaurantRepository.GetById(restId);
                    var invoiceNumber = $"WI{++restaurant.LastWarehouseInsertInvoiceNumber:0000000000}";
                    _unitOfWork.Save();
                    transaction.Commit();
                    return invoiceNumber;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public string GetNewWarehouseTransferNumber(long restId)
        {
            using (var transaction = _unitOfWork.CreateTransaction())
            {
                try
                {
                    var restaurant = _unitOfWork.RestaurantRepository.GetById(restId);
                    var invoiceNumber = $"WT{++restaurant.LastWarehouseTransferInvoiceNumber:0000000000}";
                    _unitOfWork.Save();
                    transaction.Commit();
                    return invoiceNumber;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}