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
    public class WarehouseController : ControllerBase
    {
        private IWarehouseService _warehouseService;
        private ILogger<RoleController> _logger;
        private IMapper _mapper;
        public WarehouseController(IWarehouseService warehouseService,
            ILogger<RoleController> logger,
            IMapper mapper)
        {
            this._warehouseService = warehouseService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouse(long id)
        {
            try
            {
                var warehouse = _warehouseService.GetWarehouse(id);
                if (warehouse == null) throw new NullReferenceException("Warehouse Not Found");
                var warehouseDto = _mapper.Map<Warehouse>(warehouse);
                _logger.LogInformation($"Warehouse Get {warehouse.Id}");
                return Ok(warehouseDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetWarehousesByRestaurantId(long RestaurantId)
        {
            try
            {
                var warehouses = _warehouseService.GetWarehousesByRestaurantId(RestaurantId);
                if (warehouses == null) throw new NullReferenceException("Warehouse Not Found");
                var warehouseDtos =_mapper.Map<List<Warehouse>,List<WarehouseDto>>(warehouses.ToList());
                _logger.LogInformation($"Warehouse Get By Restaurant Id:{RestaurantId}");
                return Ok(warehouseDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddWarehouse([FromBody]WarehouseDto WarehouseDto)
        {
            try
            {
                var Warehouse = _mapper.Map<Warehouse>(WarehouseDto);
                var addedWarehouse = _warehouseService.InsertWarehouse(Warehouse);
                _logger.LogInformation($"Warehouse Added {Warehouse.Id}");
                return Ok(_mapper.Map<WarehouseDto>(addedWarehouse));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateWarehouse([FromBody]WarehouseDto WarehouseDto)
        {
            try
            {
                var updateWarehouse = _mapper.Map<Warehouse>(WarehouseDto);
                var result =_warehouseService.UpdateWarehouse(updateWarehouse);
                _logger.LogInformation($"Warehouse Updated : Firstname - {updateWarehouse.Id}");
                return Ok(_mapper.Map<WarehouseDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}