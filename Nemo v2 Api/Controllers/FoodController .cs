﻿using System;
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
    public class FoodController : ControllerBase
    {
        private IFoodService _foodService;
        private ILogger<FoodController> _logger;
        private IMapper _mapper;
        public FoodController(IFoodService foodService,
            ILogger<FoodController> logger,
            IMapper mapper)
        {
            this._foodService = foodService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood(long id)
        {
            try
            {
                var food = _foodService.GetFood(id);
                if (food == null) throw new NullReferenceException("Food Not Found");
                var foodDto = _mapper.Map<FoodDto>(food);
                _logger.LogInformation($"User Get {food.Id}");
                return Ok(foodDto);
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
                var foods = _foodService.GetFoodByRestaurantId(RestaurantId);
                if (foods == null) throw new NullReferenceException("Food Not Found");
                var foodsDto =_mapper.Map<List<Food>,List<FoodDto>>(foods.ToList());
                _logger.LogInformation($"Foods Get By Restaurant Id:{RestaurantId}");
                return Ok(foodsDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddFood([FromBody]FoodDto foodDto)
        {
            try
            {
                var food = _mapper.Map<Food>(foodDto);
                var addedFood = _foodService.InsertFood(food);
                _logger.LogInformation($"Food Added {food.Id}");
                return Ok(_mapper.Map<FoodDto>(addedFood));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateFood([FromBody]FoodDto foodDto)
        {
            try
            {
                var updatedFood = _mapper.Map<Food>(foodDto);
                var result =_foodService.UpdateFood(updatedFood);
                _logger.LogInformation($"Food Updated : {updatedFood.Name}");
                return Ok(_mapper.Map<FoodDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}