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
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly ILogger<PaymentTypeController> _logger;
        private readonly IMapper _mapper;

        public PaymentTypeController(IPaymentTypeService PaymentTypeService,
            ILogger<PaymentTypeController> logger,
            IMapper mapper)
        {
            this._paymentTypeService = PaymentTypeService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentType(long id)
        {
            try
            {
                var PaymentType = _paymentTypeService.GetPaymentType(id);
                if (PaymentType == null) throw new NullReferenceException("PaymentType Not Found");
                var PaymentTypeDto = _mapper.Map<PaymentTypeDto>(PaymentType);
                _logger.LogInformation($"PaymentType Get {PaymentType.Id}");
                return Ok(PaymentTypeDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetPaymentTypeByRestaurantId(long restaurantId)
        {
            try
            {
                var PaymentTypes = _paymentTypeService.GetPaymentTypeByRestaurantId(restaurantId);
                if (PaymentTypes == null) throw new NullReferenceException("PaymentType Not Found");
                var PaymentTypesDtos = _mapper.Map<List<PaymentType>, List<PaymentTypeDto>>(PaymentTypes.ToList());
                _logger.LogInformation($"PaymentTypes Get By Restaurant Id:{restaurantId}");
                return Ok(PaymentTypesDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentType([FromBody] PaymentTypeDto PaymentTypeDto)
        {
            try
            {
                var PaymentType = _mapper.Map<PaymentType>(PaymentTypeDto);
                var addedPaymentType = _paymentTypeService.InsertPaymentType(PaymentType);
                _logger.LogInformation($"PaymentType Added {PaymentType.Id}");
                return Ok(_mapper.Map<PaymentTypeDto>(addedPaymentType));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaymentType([FromBody] PaymentTypeDto PaymentTypeDto)
        {
            try
            {
                var updatePaymentType = _mapper.Map<PaymentType>(PaymentTypeDto);
                var result = _paymentTypeService.UpdatePaymentType(updatePaymentType);
                _logger.LogInformation($"PaymentType Updated : Id: {updatePaymentType.Id}");
                return Ok(_mapper.Map<PaymentTypeDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}