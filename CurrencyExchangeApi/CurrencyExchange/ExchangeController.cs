using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeApi.CurrencyExchange
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IRates _rates;
        private readonly IValidator<QuoteQuery> _quoteValidator;

        public ExchangeController(IRates rates, IValidator<QuoteQuery> quoteValidator)
        {
            _rates = rates;
            _quoteValidator = quoteValidator;
        }

        [HttpGet("/quote")]
        public async Task<IActionResult> Get([FromQuery] QuoteQuery query)
        {
            var queryValidation = await _quoteValidator.ValidateAsync(query);
            if (queryValidation.IsValid is false)
            {
                return BadRequest(queryValidation.Errors?.Select(e => new ValidationResult()
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

            return Ok(exchange);
        }
    }
}
