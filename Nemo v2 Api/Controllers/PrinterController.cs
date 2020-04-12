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
    public class PrinterController : ControllerBase
    {
        private readonly IPrinterService _printerService;
        private readonly ILogger<PrinterController> _logger;
        private readonly IMapper _mapper;
        public PrinterController(IPrinterService printerService,
            ILogger<PrinterController> logger,
            IMapper mapper)
        {
            this._printerService = printerService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrinter(long id)
        {
            try
            {
                var printer = _printerService.GetPrinter(id);
                if (printer == null) throw new NullReferenceException("Printer Not Found");
                var printerDto = _mapper.Map<PrinterDto>(printer);
                _logger.LogInformation($"Printer Get {printer.Id}");
                return Ok(printerDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        
        [HttpGet("RestId/{RestaurantId}")]
        public async Task<IActionResult> GetPrinterByRestaurantId(long RestaurantId)
        {
            try
            {
                var printers = _printerService.GetPrinterByRestaurantId(RestaurantId);
                if (printers == null) throw new NullReferenceException("Printer Not Found");
                var printersDto =_mapper.Map<List<Printer>,List<PrinterDto>>(printers.ToList());
                _logger.LogInformation($"Printers Get By Restaurant Id:{RestaurantId}");
                return Ok(printersDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPrinter([FromBody]PrinterDto printerDto)
        {
            try
            {
                var printer = _mapper.Map<Printer>(printerDto);
                var addedPrinter = _printerService.InsertPrinter(printer);
                _logger.LogInformation($"Printer Added {printer.Id}");
                return Ok(_mapper.Map<PrinterDto>(addedPrinter));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdatePrinter([FromBody]PrinterDto printerDto)
        {
            try
            {
                var updatedPrinter = _mapper.Map<Printer>(printerDto);
                var result =_printerService.UpdatePrinter(updatedPrinter);
                _logger.LogInformation($"Printer Updated : {updatedPrinter.Name}");
                return Ok(_mapper.Map<PrinterDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}