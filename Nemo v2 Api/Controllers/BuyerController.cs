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
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerService;
        private readonly ILogger<BuyerController> _logger;
        private readonly IMapper _mapper;

        public BuyerController(IBuyerService buyerService,
            ILogger<BuyerController> logger,
            IMapper mapper)
        {
            this._buyerService = buyerService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuyer(long id)
        {
            try
            {
                var buyer = _buyerService.GetBuyer(id);
                if (buyer == null) throw new NullReferenceException("Buyer Not Found");
                var buyerDto = _mapper.Map<BuyerDto>(buyer);
                _logger.LogInformation($"Buyer Get {buyer.Id}");
                return Ok(buyerDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetBuyerByRestaurantId(long RestaurantId)
        {
            try
            {
                var buyers = _buyerService.GetBuyersByRestaurantId(RestaurantId);
                if (buyers == null) throw new NullReferenceException("Buyer Not Found");
                var buyersDtos = _mapper.Map<List<Buyer>, List<BuyerDto>>(buyers.ToList());
                _logger.LogInformation($"Buyers Get By Restaurant Id:{RestaurantId}");
                return Ok(buyersDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBuyer([FromBody] BuyerDto buyerDto)
        {
            try
            {
                var buyer = _mapper.Map<Buyer>(buyerDto);
                var addedBuyer = _buyerService.InsertBuyer(buyer);
                _logger.LogInformation($"Buyer Added {buyer.Id}");
                return Ok(_mapper.Map<BuyerDto>(addedBuyer));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBuyer([FromBody] BuyerDto buyerDto)
        {
            try
            {
                var updateBuyer = _mapper.Map<Buyer>(buyerDto);
                var result = _buyerService.UpdateBuyer(updateBuyer);
                _logger.LogInformation($"Buyer Updated : Id: {updateBuyer.Id}");
                return Ok(_mapper.Map<BuyerDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}