using Assessment.Application.Interfaces;
using Assessment.Application.Services;
using Assessment.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//STEP 4 - Set the root password for mysql connection as an environment variable
var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
string sqlConnection = builder.Configuration.GetConnectionString("MySqlConnection");

string connectionString = "";

if (!string.IsNullOrEmpty(password))
{
    connectionString = sqlConnection + password + ";";
}
else
{
    connectionString = sqlConnection;
}

var key = "ThisIsASecretKeyForJwtToken"; // Use a secure key
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// Registering services
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySQL(connectionString));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddAutoMapper(typeof(Program));  // AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfileService));

builder.Services.AddControllers();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy.WithOrigins("http://localhost:8080")  // Frontend URL
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});

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

app.UseCors("AllowLocalhost");  // Apply CORS policy here

app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();  // Ensure API controllers are mapped

app.Run();
