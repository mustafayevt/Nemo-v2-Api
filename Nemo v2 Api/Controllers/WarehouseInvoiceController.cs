using System;
using System.Collections.Generic;
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
    public class WarehouseInvoiceController : Controller
    {
        private readonly IWarehouseInvoiceService _warehouseInvoiceService;
        private readonly ILogger<WarehouseInvoiceController> _logger;
        private readonly IMapper _mapper;
        public WarehouseInvoiceController(IWarehouseInvoiceService warehouseInvoiceService,
            ILogger<WarehouseInvoiceController> logger,
            IMapper mapper)
        {
            this._warehouseInvoiceService =warehouseInvoiceService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>  GetWarehouseInvoice(long id)
        {
            try
            {
                var warehouseInvoice = _warehouseInvoiceService.GetWarehouseInvoice(id);
                if (warehouseInvoice == null) throw new NullReferenceException("WarehouseInvoice Not Found");
                var warehouseInvoiceDto = _mapper.Map<WarehouseInvoiceDto>(warehouseInvoice);
                _logger.LogInformation($"WarehouseInvoice Get {warehouseInvoice.Id}");
                return Ok(warehouseInvoiceDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult>  GetWarehouseInvoiceByRestaurantId(long id)
        {
            try
            {
                var warehouseInvoices = _warehouseInvoiceService.GetWarehouseInvoiceByRestaurantId(id);
                if (warehouseInvoices == null) throw new NullReferenceException("WarehouseInvoice Not Found");
                var warehouseInvoiceDtos = _mapper.Map<List<WarehouseInvoiceDto>>(warehouseInvoices);
                _logger.LogInformation($"WarehouseInvoice Get by RestaurantId {id}");
                return Ok(warehouseInvoiceDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        

        [HttpPost]
        public async Task<IActionResult> AddWarehouseInvoice([FromBody] WarehouseInvoiceDto warehouseInvoiceDto)
        {
            try
            {
                var warehouseInvoice = _mapper.Map<WarehouseInvoice>(warehouseInvoiceDto);
                var addedWarehouseInvoice = _warehouseInvoiceService.InsertWarehouseInvoice(warehouseInvoice);
                _logger.LogInformation($"WarehouseInvoice Added {warehouseInvoice.Id}");
                return Ok(_mapper.Map<WarehouseInvoiceDto>(addedWarehouseInvoice));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateWarehouseInvoice([FromBody] WarehouseInvoiceDto warehouseInvoiceDto)
        {
            try
            {
                var warehouseInvoice = _mapper.Map<WarehouseInvoice>(warehouseInvoiceDto);
                var updateWarehouseInvoice = _warehouseInvoiceService.UpdateWarehouseInvoice(warehouseInvoice);
                _logger.LogInformation($"WarehouseInvoice Updated {warehouseInvoice.Id}");
                return Ok(_mapper.Map<WarehouseInvoiceDto>(updateWarehouseInvoice));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}