using PriceComparison.WebApi.Endpoins;
using PriceComparison.WebApi.ExceptionHandlers;
using PriceComparison.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services
    .AddExceptionHandler<ValidationExceptionHandler>()
    .AddProblemDetails();

// Add Environment Variables
builder.Configuration
    .AddEnvironmentVariables();

// Register services
builder.Services
    .AddPriceComparisonServices(builder.Configuration)
    .AddSwagger();

var app = builder.Build();

app.UseExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigration();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.AddUserEndpoints();

app.Run();
