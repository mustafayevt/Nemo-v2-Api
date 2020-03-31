using System;
using System.Collections.Generic;
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
    public class IngredientCategoryController : Controller
    {
        private readonly IIngredientCategoryService _ingredientCategoryService;
        private readonly ILogger<IngredientCategoryController> _logger;
        private readonly IMapper _mapper;
        public IngredientCategoryController(
            IIngredientCategoryService ingredientCategoryService,
            ILogger<IngredientCategoryController> logger,
            IMapper mapper)
        {
            this._ingredientCategoryService =ingredientCategoryService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult>  GetIngredientCategory(long id)
        {
            try
            {
                var ingredientCategory = _ingredientCategoryService.GetIngredientCategory(id);
                if (ingredientCategory == null) throw new NullReferenceException("Ingredient Category Not Found");
                var ingredientCategoryDto = _mapper.Map<IngredientCategoryDto>(ingredientCategory);
                _logger.LogInformation($"Ingredient Get {ingredientCategory.Id}");
                return Ok(ingredientCategoryDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult>  GetIngredientCategoryByRestaurantId(long id)
        {
            try
            {
                var ingredientCategories = _ingredientCategoryService.GetIngredientCategoryByRestaurantId(id);
                if (ingredientCategories == null) throw new NullReferenceException("Ingredient Category Not Found");
                var ingredientCategoryDtos = _mapper.Map<List<IngredientCategoryDto>>(ingredientCategories);
                _logger.LogInformation($"Ingredient Get by RestaurantId: {id}");
                return Ok(ingredientCategoryDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddIngredientCategory([FromBody] IngredientCategoryDto ingredientCategoryDto)
        {
            try
            {
                var ingredientCategory = _mapper.Map<IngredientCategory>(ingredientCategoryDto);
                var insertIngredientCategory = _ingredientCategoryService.InsertIngredientCategory(ingredientCategory);
                _logger.LogInformation($"Ingredient Category Added {ingredientCategory.Id}");
                return Ok(_mapper.Map<IngredientCategoryDto>(insertIngredientCategory));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateIngredientCategory([FromBody] IngredientCategoryDto ingredientCategoryDto)
        {
            try
            {
                var ingredientCategory = _mapper.Map<IngredientCategory>(ingredientCategoryDto);
                var insertIngredientCategory = _ingredientCategoryService.UpdateIngredientCategory(ingredientCategory);
                _logger.LogInformation($"Ingredient Category Updated {ingredientCategory.Id}");
                return Ok(_mapper.Map<IngredientCategoryDto>(insertIngredientCategory));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}