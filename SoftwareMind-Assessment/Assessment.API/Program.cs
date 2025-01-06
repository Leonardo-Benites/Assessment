using Assessment.Application.Interfaces;
using Assessment.Application.Services;
using Assessment.Infrastructure.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
string sqlConnection = builder.Configuration.GetConnectionString("MySqlConnection");

string connectionString = sqlConnection + password + ";";

// Registering services
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySQL(connectionString));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddAutoMapper(typeof(Program));  // AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfileService));

builder.Services.AddControllers();

// Add Swagger service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Enable Swagger UI in Development
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.RoutePrefix = string.Empty;  // Serve Swagger at root (http://localhost)
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();  // Ensure API controllers are mapped

app.Run();
