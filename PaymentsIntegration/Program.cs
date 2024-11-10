using Microsoft.EntityFrameworkCore;
using PaymentsIntegration.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Database connection
var connectionString = builder.Configuration.GetConnectionString("PaymentsConnection");
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Connection string 'PaymentsConnection' not found.");

builder.Services.AddDbContext<PaymentsDbContext>(opt =>
    opt.UseMySQL(connectionString)
);

// Swagger configuration
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

app.Run();