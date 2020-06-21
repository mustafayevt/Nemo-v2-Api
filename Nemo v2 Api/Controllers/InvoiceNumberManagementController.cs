using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceNumberManagementController : ControllerBase
    {
        private readonly IInvoiceNumberManagerService _invoiceNumberManagerService;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceNumberManagementController(IInvoiceNumberManagerService invoiceNumberManagerService,
            ILogger<InvoiceController> logger)
        {
            _invoiceNumberManagerService = invoiceNumberManagerService;
            _logger = logger;
        }

        [HttpGet("{RestId}")]
        public async Task<IActionResult> GetInvoiceNumber(long RestId)
        {
            return Ok(_invoiceNumberManagerService.GetNewInvoiceNumber(RestId));
        }
        
        [HttpGet("{RestId}")]
        public async Task<IActionResult> GetWarehouseExportInvoiceNumber(long RestId)
        {
            return Ok(_invoiceNumberManagerService.GetNewWarehouseExportNumber(RestId));
        }
        
        [HttpGet("{RestId}")]
        public async Task<IActionResult> GetWarehouseInsertInvoiceNumber(long RestId)
        {
            return Ok(_invoiceNumberManagerService.GetNewWarehouseInsertNumber(RestId));
        }
        
        [HttpGet("{RestId}")]
        public async Task<IActionResult> GetWarehouseTransferInvoiceNumber(long RestId)
        {
            return Ok(_invoiceNumberManagerService.GetNewWarehouseTransferNumber(RestId));
        }
    }
}