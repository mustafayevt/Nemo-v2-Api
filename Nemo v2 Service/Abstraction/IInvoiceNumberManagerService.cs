namespace Nemo_v2_Service.Abstraction
{
    public interface IInvoiceNumberManagerService
    {
        string GetNewInvoiceNumber(long restId);
        string GetNewWarehouseExportNumber(long restId);
        string GetNewWarehouseInsertNumber(long restId);
        string GetNewWarehouseTransferNumber(long restId);
    }
}