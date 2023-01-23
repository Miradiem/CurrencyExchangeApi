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
        private readonly ILogger<ExchangeController> _logger;
        private readonly IRates _rates;
        private readonly IValidator<QuoteQuery> _quoteValidator;

        public ExchangeController(ILogger<ExchangeController> logger, IRates rates, IValidator<QuoteQuery> quoteValidator)
        {
            _logger = logger;
            _rates = rates;
            _quoteValidator = quoteValidator;
        }

        [HttpGet("/quote")]
        public async Task<IActionResult> Get([FromQuery] QuoteQuery query)
        {
            _logger.LogInformation("Currency exchange executing...");

            var queryValidation = await _quoteValidator.ValidateAsync(query);
            if (queryValidation.IsValid is false)
            {
                return BadRequest(queryValidation.Errors?.Select(error => new ValidationResult()
                {
                    Code = error.ErrorCode,
                    PropertyName = error.PropertyName,
                    Message = error.ErrorMessage
                }));
            }
            
            var rates = await _rates.GetRates(query.BaseCurrency);
            var exchange = new Conversion().GetExchange(rates, query.QuoteCurrency, query.BaseAmount);

            return Ok(exchange);
        }
    }
}