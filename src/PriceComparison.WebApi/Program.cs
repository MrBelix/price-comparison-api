using PriceComparison.WebApi.Endpoins;
using PriceComparison.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(op =>
{
    op.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add Environment Variables
builder.Configuration
    .AddEnvironmentVariables();

// Register services
builder.Services
    .AddPriceComparisonServices(builder.Configuration)
    .AddSwagger();


var app = builder.Build();

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
