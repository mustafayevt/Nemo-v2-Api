﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Api.Hubs.Models;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Helper;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<InvoiceController> _logger;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceService invoiceService,
            ILogger<InvoiceController> logger,
            IMapper mapper)
        {
            this._invoiceService = invoiceService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(long id)
        {
            try
            {
                var invoice = _invoiceService.GetInvoice(id);
                if (invoice == null) throw new NullReferenceException("Invoice Not Found");
                var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
                _logger.LogInformation($"Invoice Get Id: {invoice.Id}");
                return Ok(invoiceDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpGet("RestId/{RestaurantId}")]
        public async Task<IActionResult> GetInvoiceByRestaurantId(long RestaurantId)
        {
            try
            {
                var invoices = _invoiceService.GetInvoicesByRestaurantId(RestaurantId);
                if (invoices == null) throw new NullReferenceException("Invoice Not Found");
                var invoicesDto = _mapper.Map<List<Invoice>, List<InvoiceDto>>(invoices.ToList());
                _logger.LogInformation($"Invoices Get By Restaurant Id:{RestaurantId}");
                return Ok(invoicesDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoiceByDate(long restaurantId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var invoices = _invoiceService.GetInvoicesByDate(restaurantId, startDate, endDate);
                if (invoices == null) throw new ArgumentException("Invoice Not Found");
                var invoicesDto = invoices.Select(y => new InvoiceModel
                {
                    Payments = y.PaymentTypeInvoiceRels.Select(h => _mapper.Map<PaymentTypeInvoiceRelDto>(h)).ToList(),
                    Tables = y.InvoiceTableRels.Select(h => _mapper.Map<TableDto>(h.Table)).ToList(),
                    Amount = y.Amount,
                    Discount = y.Discount,
                    Id = y.InvoiceNumber,
                    ClosedUser = _mapper.Map<UserDto>(y.ClosedUser),
                    InvoiceNumber = y.InvoiceNumber,
                    OpenedTime = y.OpenedTime,
                    ClosedTime = y.ClosedTime,
                    OpenedUser = _mapper.Map<UserDto>(y.OpenedUser),
                    PeopleCount = y.PeopleCount,
                    RestaurantId = y.RestaurantId,
                    SectionId = y.SectionId,
                    Section = _mapper.Map<SectionDto>(y.Section),
                    ServiceCharge = y.ServiceCharge,
                    TotalAmount = y.TotalAmount
                }).ToList();
                for (int i = 0; i < invoices.Count; i++)
                {
                    var invoiceFoodModels = new List<InvoiceFoodModel>();
                    foreach (var foodInvoice in invoices[i].Foods) 
                    {
                        invoiceFoodModels.AddRange(foodInvoice.FoodInvoiceProperties.Select(y=>new InvoiceFoodModel
                        {
                            Count = y.Count,
                            IsGift = y.FoodSaleType == FoodSaleType.Gift,
                            IsNonPayable = y.FoodSaleType == FoodSaleType.NotPaid,
                            Confrimed = y.FoodSaleType == FoodSaleType.Normal,
                            ChangedPrice = y.ChangedPrice,
                            Size = y.Portion,
                            User = _mapper.Map<UserDto>(y.User),
                            Name = foodInvoice.Food.Name,
                            Id = foodInvoice.FoodId,
                            OriginalPrice = y.OriginalPrice,
                            OwnerTable = _mapper.Map<TableDto>(y.Table)
                        }));   
                    }
                    invoicesDto[i].InvoiceFoodViewModels = new ObservableCollection<InvoiceFoodModel>(invoiceFoodModels);
                }
                _logger.LogInformation(
                    $"Invoices Get By Restaurant Id:{restaurantId}, startDate:{startDate.Date}, endDate:{endDate.Date}");
                return Ok(invoicesDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] InvoiceDto invoiceDto, bool decreaseIngredients)
        {
            try
            {
                var invoice = _mapper.Map<Invoice>(invoiceDto);
                invoice.IsIngredientReduced = decreaseIngredients;
                var addedInvoice = _invoiceService.InsertInvoice(invoice);
                _logger.LogInformation($"Invoice Added {invoice.Id}");
                return Ok(_mapper.Map<InvoiceDto>(addedInvoice));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReduceIngredientsInInvoiceByDate(long restId, DateTime startDate,
            DateTime endDate)
        {
            _logger.LogInformation(
                $"Reduce ingredients in invoice by restaurandId:{restId}, startDate:{startDate.Date}, endDate:{endDate.Date}");
            var reducedInvoiceCount =
                await _invoiceService.ReduceIngredientsInInvoiceByDate(restId, startDate, endDate);
            return Ok($"Reduced Invoice count : {reducedInvoiceCount}");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                var updatedInvoice = _mapper.Map<Invoice>(invoiceDto);
                var result = _invoiceService.UpdateInvoice(updatedInvoice);
                _logger.LogInformation($"Invoice Updated Id: {updatedInvoice.Id}");
                return Ok(_mapper.Map<InvoiceDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}