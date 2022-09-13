using CurrencyExchangeApi.App;
using CurrencyExchangeApi.Storring;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeApi.CurrencyExchange
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly ILRUCache _cache;
        private readonly QueryValidator _validator;
       
        public ExchangeController(ILRUCache cache, QueryValidator validator)
        {
            _cache = cache;
            _validator = validator;
        }

        [HttpGet("/quote")]
        public async Task<IActionResult> Get([FromQuery] QuoteQuery query)
        {
            var result = await _validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var exchange = await new Conversion(_cache, query).GetExchange();
            if (exchange.ExchangeRate is null && exchange.QuoteAmount is null)
            {
                throw new KeyNotFoundException("Exchange not found.");
            }

            await new ExchangeFile("Storring/exchange-data.txt").Save(exchange);

            return Ok(exchange);
        }
    }
}
