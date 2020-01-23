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

        public TradeController(IEndpointInstance endpointInstance) {
            _endpointInstance = endpointInstance;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
                public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

            try {

                OMSTradeData tradeMessage = JsonConvert.DeserializeObject<OMSTradeData>(value);
                var result = _endpointInstance.Publish(tradeMessage);

        }
            catch {

            // push error

            }


        }


    }
}
