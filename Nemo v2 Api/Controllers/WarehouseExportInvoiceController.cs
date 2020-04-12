using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Helper;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseExportInvoiceController : Controller
    {
        private readonly IWarehouseExportInvoiceService _warehouseExportInvoiceService;
        private readonly ILogger<WarehouseExportInvoiceController> _logger;
        private readonly IMapper _mapper;
        public WarehouseExportInvoiceController(IWarehouseExportInvoiceService warehouseExportInvoiceService,
            ILogger<WarehouseExportInvoiceController> logger,
            IMapper mapper)
        {
            this._warehouseExportInvoiceService =warehouseExportInvoiceService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>  GetWarehouseExportInvoice(long id)
        {
            try
            {
                var warehouseExportInvoice = _warehouseExportInvoiceService.GetWarehouseExportInvoice(id);
                if (warehouseExportInvoice == null) throw new NullReferenceException("WarehouseExportInvoice Not Found");
                var warehouseExportInvoiceDto = _mapper.Map<WarehouseExportInvoiceDto>(warehouseExportInvoice);
                _logger.LogInformation($"WarehouseExportInvoice Get {warehouseExportInvoice.Id}");
                return Ok(warehouseExportInvoiceDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult>  GetWarehouseExportInvoiceByRestaurantId(long id)
        {
            try
            {
                var warehouseExportInvoices = _warehouseExportInvoiceService.GetWarehouseExportInvoiceByRestaurantId(id);
                if (warehouseExportInvoices == null) throw new NullReferenceException("WarehouseExportInvoice Not Found");
                var warehouseExportInvoiceDtos = _mapper.Map<List<WarehouseExportInvoiceDto>>(warehouseExportInvoices);
                _logger.LogInformation($"WarehouseExportInvoice Get by RestaurantId {id}");
                return Ok(warehouseExportInvoiceDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        

        [HttpPost]
        public async Task<IActionResult> AddWarehouseExportInvoice([FromBody] WarehouseExportInvoiceDto warehouseExportInvoiceDto)
        {
            try
            {
                var warehouseExportInvoice = _mapper.Map<WarehouseExportInvoice>(warehouseExportInvoiceDto);
                var addedWarehouseExportInvoice = _warehouseExportInvoiceService.InsertWarehouseExportInvoice(warehouseExportInvoice);
                _logger.LogInformation($"WarehouseExportInvoice Added {warehouseExportInvoice.Id}");
                return Ok(_mapper.Map<WarehouseExportInvoiceDto>(addedWarehouseExportInvoice));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateWarehouseExportInvoice([FromBody] WarehouseExportInvoiceDto warehouseExportInvoiceDto)
        {
            try
            {
                var warehouseExportInvoice = _mapper.Map<WarehouseExportInvoice>(warehouseExportInvoiceDto);
                var updateWarehouseExportInvoice = _warehouseExportInvoiceService.UpdateWarehouseExportInvoice(warehouseExportInvoice);
                _logger.LogInformation($"WarehouseExportInvoice Updated {warehouseExportInvoice.Id}");
                return Ok(_mapper.Map<WarehouseExportInvoiceDto>(updateWarehouseExportInvoice));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }
    }
}