using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<InvoiceController> _logger;
        private readonly IMapper _mapper;
        public InvoiceController(IInvoiceService invoiceService,
            ILogger<InvoiceController> logger,
            IMapper mapper)
        {
            this._invoiceService = invoiceService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(long id)
        {
            try
            {
                var invoice = _invoiceService.GetInvoice(id);
                if (invoice == null) throw new NullReferenceException("Invoice Not Found");
                var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
                _logger.LogInformation($"Invoice Get Id: {invoice.Id}");
                return Ok(invoiceDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpGet("RestId/{RestaurantId}")]
        public async Task<IActionResult> GetInvoiceByRestaurantId(long RestaurantId)
        {
            try
            {
                var invoices = _invoiceService.GetInvoicesByRestaurantId(RestaurantId);
                if (invoices == null) throw new NullReferenceException("Invoice Not Found");
                var invoicesDto =_mapper.Map<List<Invoice>,List<InvoiceDto>>(invoices.ToList());
                _logger.LogInformation($"Invoices Get By Restaurant Id:{RestaurantId}");
                return Ok(invoicesDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody]InvoiceDto invoiceDto)
        {
            try
            {
                var invoice = _mapper.Map<Invoice>(invoiceDto);
                var addedInvoice = _invoiceService.InsertInvoice(invoice);
                _logger.LogInformation($"Invoice Added {invoice.Id}");
                return Ok(_mapper.Map<InvoiceDto>(addedInvoice));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateInvoice([FromBody]InvoiceDto invoiceDto)
        {
            try
            {
                var updatedInvoice = _mapper.Map<Invoice>(invoiceDto);
                var result =_invoiceService.UpdateInvoice(updatedInvoice);
                _logger.LogInformation($"Invoice Updated Id: {updatedInvoice.Id}");
                return Ok(_mapper.Map<InvoiceDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}