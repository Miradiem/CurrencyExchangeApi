using FluentValidation;

namespace CurrencyExchangeApi.App
{
    public class QuoteValidator : AbstractValidator<QuoteQuery>
    {
        private readonly List<string> _currencies = new List<string>() { "USD", "EUR", "GBP", "ILS" };

        public QuoteValidator()
        {
            RuleFor(quote => quote.BaseCurrency)
                .Must(quote => _currencies.Contains(quote))
                .WithMessage("Wrong input. Viable currencies: " + String.Join(",", _currencies) + ".");

            RuleFor(quote => quote.QuoteCurrency)
                .Must(quote => _currencies.Contains(quote))
                .WithMessage("Wrong input. Viable currencies: " + String.Join(",", _currencies) + ".");

            RuleFor(quote => quote.BaseAmount)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Can't be less than 1.");
        } 
    } 
}
