using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NServiceBus;
using TradeDataFeed.Contexts;

namespace TradeDataAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : Controller
    {

        public IEndpointInstance _endpointInstance;

        public TradeController(IEndpointInstance endpointInstance)
        {
            _endpointInstance = endpointInstance;
        }

        // GET api/values
        [HttpGet("{identifer}")]
        public IActionResult Get(string identifer)
        {

            var data = new TradeContext();
            var record1 = data.TradeData.FirstOrDefault(t => t.Identifier == identifer);

            //    var data = new List<OMSTradeData>
            return Json(record1);
        }


        [HttpPost]
        public void PostTrades([FromBody] string tradeStream)
        {

            var message = new TradeMessage(tradeStream);

            try
            {
                List<OMSTradeData> trades = JsonConvert.DeserializeObject<List<OMSTradeData>>(tradeStream);
                foreach (var trade in trades)
                {
                    var tradeResult = _endpointInstance.Publish(trade);
                }
            }
            catch
            {
                message.ValidityType = MessageValidityType.Failure;
            }

            var messageResult = _endpointInstance.Publish(message);

        }
    }
}
