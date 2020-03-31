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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly ILogger<SupplierController> _logger;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierService supplierService,
            ILogger<SupplierController> logger,
            IMapper mapper)
        {
            this._supplierService = supplierService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(long id)
        {
            try
            {
                var supplier = _supplierService.GetSupplier(id);
                if (supplier == null) throw new NullReferenceException("Supplier Not Found");
                var supplierDto = _mapper.Map<SupplierDto>(supplier);
                _logger.LogInformation($"Supplier Get {supplier.Id}");
                return Ok(supplierDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetSupplierByRestaurantId(long RestaurantId)
        {
            try
            {
                var suppliers = _supplierService.GetSuppliersByRestaurantId(RestaurantId);
                if (suppliers == null) throw new NullReferenceException("Supplier Not Found");
                var suppliersDtos = _mapper.Map<List<Supplier>, List<SupplierDto>>(suppliers.ToList());
                _logger.LogInformation($"Suppliers Get By Restaurant Id:{RestaurantId}");
                return Ok(suppliersDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierDto supplierDto)
        {
            try
            {
                var supplier = _mapper.Map<Supplier>(supplierDto);
                var addedSupplier = _supplierService.InsertSupplier(supplier);
                _logger.LogInformation($"Supplier Added {supplier.Id}");
                return Ok(_mapper.Map<SupplierDto>(addedSupplier));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierDto supplierDto)
        {
            try
            {
                var updateSupplier = _mapper.Map<Supplier>(supplierDto);
                var result = _supplierService.UpdateSupplier(updateSupplier);
                _logger.LogInformation($"Supplier Updated : Id: {updateSupplier.Id}");
                return Ok(_mapper.Map<SupplierDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}