using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using finance_reporter_api.Data;
using finance_reporter_api.Services.AuthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder();
var services = builder.Services;
var allowMyOrigins = "AllowMyOrigins";

builder.Logging.ClearProviders();

// Add Azure App Configuration 
var connectionString = builder.Configuration.GetConnectionString("AzureAppConfiguration");
configBuilder.AddAzureAppConfiguration(connectionString);

var configuration = configBuilder.Build();

// Add logging
builder.Logging.AddConsole();
builder.Logging.AddEventSourceLogger();

// Turn off claim mapping for Microsoft middleware
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add services to the container.
services.AddDbContext<DataContext>(
    options =>
    {
        options.UseSqlServer(configuration["ConnectionString"]);
    },
        ServiceLifetime.Transient
);

services.AddHttpClient();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            Description =
                "Standard Authorization header using the Bearer scheme, e.g. \"bearer {token} \"",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
    );
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

services.AddSwaggerGenNewtonsoftSupport();
services.AddAutoMapper(typeof(Program).Assembly);
services.AddSingleton<IConfiguration>(configuration);

services.AddScoped<IAuthService, AuthService>();
services.AddTransient<finance_reporter_api.Logger.ILogger, finance_reporter_api.Logger.Logger>();

// Authentication
services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(configuration["SecretKey"])
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

services.AddHttpContextAccessor();
services.AddSingleton<IPrincipal>(
    provider => provider.GetService<IHttpContextAccessor>().HttpContext.User
);


services.AddCors(options =>
{
    options.AddPolicy(
        allowMyOrigins,
        builder =>
        {
            builder
                .WithOrigins("http://localhost:3000", "https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowMyOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
