using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _cache;

        public TradeController(IEndpointInstance endpointInstance, IMemoryCache memoryCache)
        {
            _endpointInstance = endpointInstance;
            _cache = memoryCache;
        }

        // GET api/values
        [HttpGet("{identifier}")]
        public IActionResult Get(string identifier)
        {

            // This has implemented fairly basic caching of data for a minute.
            // In a production environment you may want to look at sql dependency injection to flush the cache only
            // if the underlying database values have changed.

            OMSTradeData cacheEntry = null;

            if (identifier != null && !_cache.TryGetValue(identifier.ToUpper(), out cacheEntry)) {
                var data = new TradeContext();
                cacheEntry = data.TradeData.FirstOrDefault(t => t.Identifier == identifier);

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
                options.SlidingExpiration = TimeSpan.FromMinutes(1);
                _cache.Set(cacheEntry.Identifier, cacheEntry, options);
            }

            return Json(cacheEntry);

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
