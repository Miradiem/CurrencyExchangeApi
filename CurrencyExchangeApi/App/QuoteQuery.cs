namespace CurrencyExchangeApi.App
{
    public class QuoteQuery
    {
        public string BaseCurrency { get; set; }

        public string QuoteCurrency { get; set; }

        public int BaseAmount { get; set; }
    }
}
