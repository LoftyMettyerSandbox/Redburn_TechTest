using System.Collections.Generic;
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


        //public void PostTrades([FromBody] string value) {

        //    try
        //    {
        //        List<OMSTradeData>messages = JsonConvert.DeserializeObject<List<OMSTradeData>>(value);
        //        foreach (var tradeMessage in messages) {
        //            var result = _endpointInstance.Publish(tradeMessage);
        //        }
                
        //    }
        //    catch
        //    {
        //        // push error
        //    }

        //}


        // POST api/values
        [HttpPost]
        public void PostTrade([FromBody] string value)
        {

            try
            {

                List<OMSTradeData>messages = JsonConvert.DeserializeObject<List<OMSTradeData>>(value);
                foreach (var tradeMessage in messages) {
                    var result = _endpointInstance.Publish(tradeMessage);
                }

            }
            catch
            {
                // push error
            }

        }



    }
}
