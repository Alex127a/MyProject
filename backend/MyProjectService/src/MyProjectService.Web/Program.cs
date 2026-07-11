using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using MyProjectService.Infrastructure.Postgres;
using MyProjectService.Infrastructure.Repositories;
using Scalar.AspNetCore;
using DotNetEnv;
using Npgsql;
using FluentValidation;
using MyProjectService.Core;
using MyProjectService.Contracts;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load(); 
}

var csb = new NpgsqlConnectionStringBuilder
{
    Host = Env.GetString("DB_HOST", "localhost"),
    Port = Env.GetInt("DB_PORT", 5433),
    Database = Env.GetString("DB_NAME"),
    Username = Env.GetString("DB_USER"),
    Password = Env.GetString("DB_PASSWORD")
};
var connectionString = csb.ConnectionString;

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(connectionString));



var repositoryTypeRaw = Env.GetString("REPOSITORY_TYPE") ?? builder.Configuration["RepositoryType"];


var repositoryType = repositoryTypeRaw?.Trim().ToUpperInvariant();

if (string.Equals(repositoryType, "EFCORE", StringComparison.Ordinal))
{
    builder.Services.AddScoped<ILocationsRepository, EfLocationsRepository>();
}
else if (string.Equals(repositoryType, "DAPPER", StringComparison.Ordinal))
{
    
    builder.Services.AddScoped<ILocationsRepository>(provider => 
        new DapperLocationsRepository(
            connectionString, 
            provider.GetRequiredService<ILogger<DapperLocationsRepository>>()
        ));
}
else
{
    
    throw new InvalidOperationException(
        $"Критическая ошибка конфигурации: Не удалось распознать тип репозитория. " +
        $"Полученное значение: '{repositoryTypeRaw}'. " +
        $"Допустимые значения: 'EfCore' или 'Dapper' в вашем файле .env или appsettings.json.");
}


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IValidator<CreateLocationDto>, CreateLocationValidator>();
builder.Services.AddScoped<LocationsService>();
builder.Services.AddHealthChecks();
builder.Services.AddScoped<CreateLocationHandler>();

var app = builder.Build();

app.MapControllers();   


if (!app.Environment.IsProduction())
{
    
    var manualOpenApiJson = """
    {
      "openapi": "3.0.0",
      "info": { "title": "Directory Service API", "version": "v1" },
      "paths": {
        "/locations": {
          "post": {
            "summary": "Создать новую локацию",
            "requestBody": {
              "required": true,
              "content": {
                "application/json": {
                  "schema": {
                    "type": "object",
                    "properties": {
                      "name": { "type": "string", "example": "Центральный Офис" },
                      "city": { "type": "string", "example": "Москва" },
                      "addressLine": { "type": "string", "example": "ул. Ленина, д. 10" }
                    },
                    "required": ["name", "city", "addressLine"]
                  }
                }
              }
            },
            "responses": {
              "200": { "description": "Успешное создание" },
              "400": { "description": "Ошибка валидации или дубликат" }
            }
          }
        }
      }
    }
    """;

  
    app.MapScalarApiReference("/scalar", options =>
    {
    
    });

    
    app.MapGet("/openapi/v1.json", () => Results.Text(manualOpenApiJson, "application/json"));
}



await app.RunAsync();

