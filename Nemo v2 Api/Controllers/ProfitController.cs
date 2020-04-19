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
    public class ProfitController : Controller
    {
        private readonly IProfitService _profitService;
        private readonly ILogger<ProfitController> _logger;
        private readonly IMapper _mapper;

        public ProfitController(IProfitService profitService,
            ILogger<ProfitController> logger,
            IMapper mapper)
        {
            this._profitService = profitService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfit(long id)
        {
            try
            {
                var Profit = _profitService.GetProfit(id);
                if (Profit == null) throw new NullReferenceException("Profit Not Found");
                var ProfitDto = _mapper.Map<ProfitDto>(Profit);
                _logger.LogInformation($"Profit Get {Profit.Id}");
                return Ok(ProfitDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetProfitByRestaurantId(long restaurantId)
        {
            try
            {
                var profits = _profitService.GetProfitByRestaurantId(restaurantId);
                if (profits == null) throw new NullReferenceException("Profit Not Found");
                var profitsDtos = _mapper.Map<List<Profit>, List<ProfitDto>>(profits.ToList());
                _logger.LogInformation($"Profits Get By Restaurant Id:{restaurantId}");
                return Ok(profitsDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProfit([FromBody] ProfitDto ProfitDto)
        {
            try
            {
                var Profit = _mapper.Map<Profit>(ProfitDto);
                var addedProfit = _profitService.InsertProfit(Profit);
                _logger.LogInformation($"Profit Added {Profit.Id}");
                return Ok(_mapper.Map<ProfitDto>(addedProfit));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfit([FromBody] ProfitDto profitDto)
        {
            try
            {
                var updateProfit = _mapper.Map<Profit>(profitDto);
                var result = _profitService.UpdateProfit(updateProfit);
                _logger.LogInformation($"Profit Updated : Id: {updateProfit.Id}");
                return Ok(_mapper.Map<ProfitDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}