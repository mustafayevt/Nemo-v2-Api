using System;
using System.Collections.Generic;
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
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private IIngredientCategoryService _ingredientCategoryService;
        private readonly ILogger<IngredientController> _logger;
        private readonly IMapper _mapper;
        public IngredientController(IIngredientService ingredientService,
            IIngredientCategoryService ingredientCategoryService,
            ILogger<IngredientController> logger,
            IMapper mapper)
        {
            this._ingredientService =ingredientService;
            this._ingredientCategoryService =ingredientCategoryService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>  GetIngredient(long id)
        {
            try
            {
                var ingredient = _ingredientService.GetIngredient(id);
                if (ingredient == null) throw new NullReferenceException("Ingredient Not Found");
                var ingredientDto = _mapper.Map<IngredientDto>(ingredient);
                _logger.LogInformation($"Ingredient Get {ingredient.Id}");
                return Ok(ingredientDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult>  GetIngredientByRestaurantId(long id)
        {
            try
            {
                var ingredients = _ingredientService.GetIngredientByRestaurantId(id);
                if (ingredients == null) throw new NullReferenceException("Ingredient Not Found");
                var ingredientDtos = _mapper.Map<List<IngredientDto>>(ingredients);
                _logger.LogInformation($"Ingredient Get by RestaurantId {id}");
                return Ok(ingredientDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult>  GetIngredientByWarehouseId(long id)
        {
            try
            {
                var ingredients = _ingredientService.GetIngredientByWarehouseId(id);
                if (ingredients == null) throw new NullReferenceException("Ingredient Not Found");
                var ingredientDtos = _mapper.Map<List<IngredientDto>>(ingredients);
                foreach (var ingredientDto in ingredientDtos)
                {
                    ingredientDto.AvgPriceByWarehouse = _ingredientService.CalculateAveragePrice(ingredientDto.Id, id);
                }
                _logger.LogInformation($"Ingredient Get by WarehouseId {id}");
                return Ok(ingredientDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        

        [HttpPost]
        public async Task<IActionResult> AddIngredient([FromBody] IngredientDto ingredientDto)
        {
            try
            {
                var ingredient = _mapper.Map<Ingredient>(ingredientDto);
                var addedIngredient = _ingredientService.InsertIngredient(ingredient);
                _logger.LogInformation($"Ingredient Added {ingredient.Id}");
                return Ok(_mapper.Map<IngredientDto>(addedIngredient));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateIngredient([FromBody] IngredientDto ingredientDto)
        {
            try
            {
                var ingredient = _mapper.Map<Ingredient>(ingredientDto);
                var updateIngredient = _ingredientService.UpdateIngredient(ingredient);
                _logger.LogInformation($"Ingredient Updated {ingredient.Id}");
                return Ok(_mapper.Map<IngredientDto>(updateIngredient));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }
    }
}