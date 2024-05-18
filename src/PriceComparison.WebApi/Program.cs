using PriceComparison.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add Environment Variables
builder.Configuration
    .AddEnvironmentVariables();

builder.Services
    .AddPriceComparisonServices(builder.Configuration);

// Add services to the container.
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

app.Run();
