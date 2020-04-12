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
    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        private readonly ILogger<TableController> _logger;
        private readonly IMapper _mapper;

        public TableController(ITableService tableService,
            ILogger<TableController> logger,
            IMapper mapper)
        {
            this._tableService = tableService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTable(long id)
        {
            try
            {
                var table = _tableService.GetTable(id);
                if (table == null) throw new NullReferenceException("table Not Found");
                var tableDto = _mapper.Map<TableDto>(table);
                _logger.LogInformation($"table Get {table.Id}");
                return Ok(tableDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetTableByRestaurantId(long RestaurantId)
        {
            try
            {
                var tables = _tableService.GetTablesByRestaurantId(RestaurantId);
                if (tables == null) throw new NullReferenceException("table Not Found");
                var tablesDtos = _mapper.Map<List<Table>, List<TableDto>>(tables.ToList());
                _logger.LogInformation($"tables Get By Restaurant Id:{RestaurantId}");
                return Ok(tablesDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        [HttpGet("{SectionId}")]
        public async Task<IActionResult> GetTableBySectionId(long SectionId)
        {
            try
            {
                var tables = _tableService.GetTablesBySectioinId(SectionId);
                if (tables == null) throw new NullReferenceException("table Not Found");
                var tablesDtos = _mapper.Map<List<Table>, List<TableDto>>(tables.ToList());
                _logger.LogInformation($"tables Get By Section Id:{SectionId}");
                return Ok(tablesDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] TableDto tableDto)
        {
            try
            {
                var table = _mapper.Map<Table>(tableDto);
                var addedtable = _tableService.InsertTable(table);
                _logger.LogInformation($"table Added {table.Id}");
                return Ok(_mapper.Map<TableDto>(addedtable));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTable([FromBody] TableDto tableDto)
        {
            try
            {
                var updatetable = _mapper.Map<Table>(tableDto);
                var result = _tableService.UpdateTable(updatetable);
                _logger.LogInformation($"table Updated : Id: {updatetable.Id}");
                return Ok(_mapper.Map<TableDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}