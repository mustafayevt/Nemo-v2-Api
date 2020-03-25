using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private IRestaurantService _restaurantService;
        private ILogger<RestaurantController> _logger;
        private IMapper _mapper;
        public RestaurantController(IRestaurantService restaurantService,
            ILogger<RestaurantController> logger,
            IMapper mapper)
        {
            this._restaurantService = restaurantService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetRestaurant(long id)
        {
            try
            {
                var restaurant = _restaurantService.GetRestaurant(id);
                if (restaurant == null) throw new NullReferenceException("Restaurant Not Found");
                var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
                _logger.LogInformation($"Restaurant Get {restaurant.Id}");
                return Ok(restaurantDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpGet("Branches/{RestaurantId}")]
        public IActionResult GetBranchesByRestaurantId(long RestaurantId)
        {
            try
            {
                var restaurants = _restaurantService.GetBranches(RestaurantId);
                if (restaurants == null) throw new NullReferenceException("Branch Not Found");
                var restaurantsDtos =_mapper.Map<List<Restaurant>,List<RestaurantDto>>(restaurants.ToList());
                _logger.LogInformation($"Branch(es) Get By Restaurant Id:{RestaurantId}");
                return Ok(restaurantsDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpPost("Add")]
        public IActionResult AddRestaurant([FromBody]RestaurantDto restaurantDto)
        {
            try
            {
                var restaurant = _mapper.Map<Restaurant>(restaurantDto);
                var addedRestaurant = _restaurantService.InsertRestaurant(restaurant);
                _logger.LogInformation($"Restaurant Added {restaurant.Id}");
                return Ok(_mapper.Map<RestaurantDto>(addedRestaurant));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateRestaurant([FromBody]RestaurantDto restaurantDto)
        {
            try
            {
                var updateRestaurant = _mapper.Map<Restaurant>(restaurantDto);
                var result =_restaurantService.UpdateRestaurant(updateRestaurant);
                _logger.LogInformation($"Restaurant Updated : RestaurantId - {updateRestaurant.Id}");
                return Ok(_mapper.Map<RestaurantDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}