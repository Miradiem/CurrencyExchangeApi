using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ILRUCache, LRUCache>();
builder.Services.AddValidatorsFromAssemblyContaining<QueryValidator>();
builder.Services.AddScoped<IRates, LatestRates>();
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