using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Data;
using Nemo_v2_Data.Currency;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Helper;
using Nemo_v2_Service.Abstraction;
using Newtonsoft.Json;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<BuyerController> _logger;
        private readonly IMapper _mapper;

        public CurrencyController(ILogger<BuyerController> logger,
            IMapper mapper)
        {
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetCurrency(string date)
        {
            try
            {
                var dateTime = DateTime.Parse(date);
                
                XmlSerializer ser = new XmlSerializer(typeof(ValCurs));
                var url = $"https://www.cbar.az/currencies/{dateTime:dd.MM.yyyy}.xml";
                using (XmlReader reader = XmlReader.Create(url))
                {
                    var currency = (ValCurs) ser.Deserialize(reader);
                    _logger.LogInformation($"Currency Get By Date {dateTime:dd.mm.yy}");
                    return Ok(currency);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}