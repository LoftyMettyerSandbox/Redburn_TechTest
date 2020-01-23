using System;
using System.Collections.Generic;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NServiceBus;

namespace TradeDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {

        public IEndpointInstance _endpointInstance;

        public TradeController(IEndpointInstance endpointInstance)
        {
            _endpointInstance = endpointInstance;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// POST api/values
        //[HttpPost]
        //public void PostTrades([FromBody] IEnumerable<string> value) {
        //    var count = 1;
        //}

        [HttpPost]
        //public void PostTrades([FromBody] IList<string> value)
        public void PostTrade([FromBody] string value)
        {

            var message = new TradeMessage(value);

            try
            {
                List<OMSTradeData> messages = JsonConvert.DeserializeObject<List<OMSTradeData>>(value);
                foreach (var tradeMessage in messages)
                {
                    var tradeResult = _endpointInstance.Publish(tradeMessage);
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
