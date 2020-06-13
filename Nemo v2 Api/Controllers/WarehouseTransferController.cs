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
using Nemo_v2_Repo.Helper;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseTransferInvoiceController : ControllerBase
    {
        private readonly IWarehouseTransferInvoiceService _warehouseTransferInvoiceService;
        private readonly ILogger<RoleController> _logger;
        private readonly IMapper _mapper;
        public WarehouseTransferInvoiceController(IWarehouseTransferInvoiceService warehouseTransferInvoiceService,
            ILogger<RoleController> logger,
            IMapper mapper)
        {
            this._warehouseTransferInvoiceService = warehouseTransferInvoiceService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseTransferInvoice(long id)
        {
            try
            {
                var warehouseTransferInvoice = _warehouseTransferInvoiceService.GetWarehouseTransferInvoice(id);
                if (warehouseTransferInvoice == null) throw new NullReferenceException("WarehouseTransferInvoice Not Found");
                var warehouseTransferInvoiceDto = _mapper.Map<WarehouseTransferInvoice>(warehouseTransferInvoice);
                _logger.LogInformation($"WarehouseTransferInvoice Get {warehouseTransferInvoice.Id}");
                return Ok(warehouseTransferInvoiceDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        
        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetWarehouseTransferInvoicesByRestaurantId(long RestaurantId)
        {
            try
            {
                var warehouseTransferInvoices = _warehouseTransferInvoiceService.GetWarehouseTransferInvoiceByRestaurantId(RestaurantId);
                if (warehouseTransferInvoices == null) throw new NullReferenceException("WarehouseTransferInvoice Not Found");
                var warehouseTransferInvoiceDtos =_mapper.Map<List<WarehouseTransferInvoice>,List<WarehouseTransferInvoiceDto>>(warehouseTransferInvoices.ToList());
                _logger.LogInformation($"WarehouseTransferInvoice Get By Restaurant Id:{RestaurantId}");
                return Ok(warehouseTransferInvoiceDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddWarehouseTransferInvoice([FromBody]List<WarehouseTransferInvoiceDto> WarehouseTransferInvoiceDtos)
        {
            try
            {
                var WarehouseTransferInvoice = _mapper.Map<WarehouseTransferInvoice>(WarehouseTransferInvoiceDtos?.First());
                var tmp = WarehouseTransferInvoice.RequesterWarehouseId;
                WarehouseTransferInvoice.RequesterWarehouseId = WarehouseTransferInvoice.AcceptorWarehouseId;
                WarehouseTransferInvoice.AcceptorWarehouseId = tmp;
                WarehouseTransferInvoice.Ingredients = new List<IngredientsTransfer>();
                foreach (var transferInvoiceDto in WarehouseTransferInvoiceDtos)
                {
                    WarehouseTransferInvoice.Ingredients.Add(_mapper.Map<IngredientsTransfer>(transferInvoiceDto));
                }
                var addedWarehouseTransferInvoice = _warehouseTransferInvoiceService.InsertWarehouseTransferInvoice(WarehouseTransferInvoice);
                _logger.LogInformation($"WarehouseTransferInvoice Added {WarehouseTransferInvoice.Id}");
                return Ok(_mapper.Map<WarehouseTransferInvoiceDto>(addedWarehouseTransferInvoice));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateWarehouseTransferInvoice([FromBody]List<WarehouseTransferInvoiceDto> WarehouseTransferInvoiceDtos)
        {
            try
            {
                var updateWarehouseTransferInvoice = _mapper.Map<WarehouseTransferInvoice>(WarehouseTransferInvoiceDtos);
                var result =_warehouseTransferInvoiceService.UpdateWarehouseTransferInvoice(updateWarehouseTransferInvoice);
                _logger.LogInformation($"WarehouseTransferInvoice Updated : Firstname - {updateWarehouseTransferInvoice.Id}");
                return Ok(_mapper.Map<WarehouseTransferInvoiceDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}