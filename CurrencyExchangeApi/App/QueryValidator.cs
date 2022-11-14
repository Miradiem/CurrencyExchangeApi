using FluentValidation;

namespace CurrencyExchangeApi.App
{
    public class QueryValidator : AbstractValidator<QuoteQuery>
    {
        private readonly List<string> _currencies = new List<string>() { "USD", "EUR", "GBP", "ILS" };

        public QueryValidator()
        {

            RuleFor(x => x.BaseCurrency)
                .Must(x => _currencies.Contains(x))
                .WithMessage("Wrong input. Viable currencies: " + String.Join(",", _currencies) + ".");

            RuleFor(x => x.QuoteCurrency)
                .Must(x => _currencies.Contains(x))
                .WithMessage("Wrong input. Viable currencies: " + String.Join(",", _currencies) + ".");

            RuleFor(x => x.BaseAmount)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Can't be less than 1.");
        }

        
} 
}
