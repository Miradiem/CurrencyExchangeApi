using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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