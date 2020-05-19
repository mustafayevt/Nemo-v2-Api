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
    public class ManualCurrencyModelController : ControllerBase
    {
        private readonly IManualCurrencyModelService _manualCurrencyModelService;
        private readonly ILogger<ManualCurrencyModelController> _logger;
        private readonly IMapper _mapper;

        public ManualCurrencyModelController(IManualCurrencyModelService manualCurrencyModelService,
            ILogger<ManualCurrencyModelController> logger,
            IMapper mapper)
        {
            this._manualCurrencyModelService = manualCurrencyModelService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManualCurrencyModel(long id)
        {
            try
            {
                var manualCurrencyModel = _manualCurrencyModelService.GetManualCurrencyModel(id);
                if (manualCurrencyModel == null) throw new NullReferenceException("ManualCurrencyModel Not Found");
                var manualCurrencyModelDto = _mapper.Map<ManualCurrencyModelDto>(manualCurrencyModel);
                _logger.LogInformation($"ManualCurrencyModel Get {manualCurrencyModel.Id}");
                return Ok(manualCurrencyModelDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetManualCurrencyModelByRestaurantId(long RestaurantId)
        {
            try
            {
                var manualCurrencyModels = _manualCurrencyModelService.GetManualCurrencyModelsByRestaurantId(RestaurantId);
                if (manualCurrencyModels == null) throw new NullReferenceException("ManualCurrencyModel Not Found");
                var manualCurrencyModelsDtos = _mapper.Map<List<ManualCurrencyModel>, List<ManualCurrencyModelDto>>(manualCurrencyModels.ToList());
                _logger.LogInformation($"ManualCurrencyModels Get By Restaurant Id:{RestaurantId}");
                return Ok(manualCurrencyModelsDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddManualCurrencyModel([FromBody] ManualCurrencyModelDto manualCurrencyModelDto)
        {
            try
            {
                var manualCurrencyModel = _mapper.Map<ManualCurrencyModel>(manualCurrencyModelDto);
                var addedManualCurrencyModel = _manualCurrencyModelService.InsertManualCurrencyModel(manualCurrencyModel);
                _logger.LogInformation($"ManualCurrencyModel Added {manualCurrencyModel.Id}");
                return Ok(_mapper.Map<ManualCurrencyModelDto>(addedManualCurrencyModel));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManualCurrencyModel([FromBody] ManualCurrencyModelDto manualCurrencyModelDto)
        {
            try
            {
                var updateManualCurrencyModel = _mapper.Map<ManualCurrencyModel>(manualCurrencyModelDto);
                var result = _manualCurrencyModelService.UpdateManualCurrencyModel(updateManualCurrencyModel);
                _logger.LogInformation($"ManualCurrencyModel Updated : Id: {updateManualCurrencyModel.Id}");
                return Ok(_mapper.Map<ManualCurrencyModelDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}