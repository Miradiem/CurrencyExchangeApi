using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddSingleton<ILRUCache, LRUCache>
    (serviceProvider => new LRUCache(4));
builder.Services.AddValidatorsFromAssemblyContaining<QuoteValidator>();
builder.Services.AddScoped<IRates, LatestRates>
    (serviceProvider => new LatestRates
        (new ApiClient("https://api.exchangerate-api.com/v4/latest")));
builder.Services.Decorate<IRates, CachedRates>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandling>();

app.Run();