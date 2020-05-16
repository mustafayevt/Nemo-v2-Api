using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Nemo_v2_Repo.DbContexts;
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
        private readonly ILogger<CurrencyController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _applicationContext;

        public CurrencyController(ILogger<CurrencyController> logger,
            IMapper mapper,
            ApplicationContext applicationContext)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._applicationContext = applicationContext;
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetCurrency(string date)
        {
            try
            {
                var dateTime = DateTime.Parse(date);

                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ValCurs));
                    var url = $"https://www.cbar.az/currencies/{dateTime:dd.MM.yyyy}.xml";
                    WebRequest request = WebRequest.Create(url);
                    request.Timeout = 7000;
                    using (WebResponse response = request.GetResponse())
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        var currency = (ValCurs) ser.Deserialize(reader);
                        System.IO.File.WriteAllText("currency.json",JsonConvert.SerializeObject(currency));
                        _logger.LogInformation($"Currency Get By Date {dateTime:dd.mm.yy}");
                        return Ok(currency);
                    }
                }
                catch (Exception e)
                {
                    return Ok(System.IO.File.ReadAllText("currency.json"));
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