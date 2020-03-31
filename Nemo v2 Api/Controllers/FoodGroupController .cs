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
    public class FoodGroupController : ControllerBase
    {
        private readonly IFoodGroupService _foodGroupService;
        private readonly ILogger<FoodGroupController> _logger;
        private readonly IMapper _mapper;
        public FoodGroupController(IFoodGroupService foodGroupService,
            ILogger<FoodGroupController> logger,
            IMapper mapper)
        {
            this._foodGroupService = foodGroupService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodGroup(long id)
        {
            try
            {
                var foodGroup = _foodGroupService.GetFoodGroup(id);
                if (foodGroup == null) throw new NullReferenceException("FoodGroup Not Found");
                var foodGroupDto = _mapper.Map<FoodGroupDto>(foodGroup);
                _logger.LogInformation($"User Get {foodGroup.Id}");
                return Ok(foodGroupDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpGet("RestId/{RestaurantId}")]
        public async Task<IActionResult> GetUserByRestaurantId(long RestaurantId)
        {
            try
            {
                var foodGroups = _foodGroupService.GetFoodGroupByRestaurantId(RestaurantId);
                if (foodGroups == null) throw new NullReferenceException("FoodGroup Not Found");
                var foodGroupsDto =_mapper.Map<List<FoodGroup>,List<FoodGroupDto>>(foodGroups.ToList());
                _logger.LogInformation($"FoodGroups Get By Restaurant Id:{RestaurantId}");
                return Ok(foodGroupsDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddFoodGroup([FromBody]FoodGroupDto foodGroupDto)
        {
            try
            {
                var foodGroup = _mapper.Map<FoodGroup>(foodGroupDto);
                var addedFoodGroup = _foodGroupService.InsertFoodGroup(foodGroup);
                _logger.LogInformation($"FoodGroup Added {foodGroup.Id}");
                return Ok(_mapper.Map<FoodGroupDto>(addedFoodGroup));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateFoodGroup([FromBody]FoodGroupDto foodGroupDto)
        {
            try
            {
                var updatedFoodGroup = _mapper.Map<FoodGroup>(foodGroupDto);
                var result =_foodGroupService.UpdateFoodGroup(updatedFoodGroup);
                _logger.LogInformation($"FoodGroup Updated : {updatedFoodGroup.Name}");
                return Ok(_mapper.Map<FoodGroupDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}