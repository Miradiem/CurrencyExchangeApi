using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using CurrencyExchangeApi.Storage;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeApi.CurrencyExchange
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IRates _rates;
        private readonly IValidator<QuoteQuery> _validator;

        public ExchangeController(IRates rates, IValidator<QuoteQuery> validator)
        {
            _rates = rates;
            _validator = validator;
        }

        [HttpGet("/quote")]
        public async Task<IActionResult> Get([FromQuery] QuoteQuery query)
        {
            var validation = await _validator.ValidateAsync(query);

            if (validation.IsValid is false)
            {
                return BadRequest(validation.Errors?.Select(e => new ValidationResult()
                {
                    Code = e.ErrorCode,
                    PropertyName = e.PropertyName,
                    Message = e.ErrorMessage
                }));
            }

            var rates = await _rates.GetRates(query.BaseCurrency);
            var exchange = new Conversion().GetExchange(rates, query.QuoteCurrency, query.BaseAmount);

            if (exchange is null)
            {
                throw new Exception();
            }
            
            await new ExchangeFile("Storage/exchange-data.txt").Save(exchange);

            return Ok(exchange);
        }
    }
}
