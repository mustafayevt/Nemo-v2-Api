namespace Nemo_v2_Api.Hubs.Models
{
    public class InvoiceDbMoel
    {
        public InvoiceDbMoel(string invoiceId,long branchId, string jsonData)
        {
            InvoiceId = invoiceId;
            JsonData = jsonData;
            BranchId = branchId;
        }

        public int Id { get; set; }
        public string InvoiceId { get; set; }
        public long BranchId { get; set; }
        public string JsonData { get; set; }
    }
}