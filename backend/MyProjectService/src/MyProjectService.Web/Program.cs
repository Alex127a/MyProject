using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using MyProjectService.Infrastructure.Postgres;
using Scalar.AspNetCore;
using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);


Env.Load("..\\..\\..\\..\\backend\\.env");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/health", () => "ОК");

app.MapControllers();   

if (!app.Environment.IsProduction())
{
    app.MapOpenApi("/openapi/{documentName}.json");
    app.MapScalarApiReference("/scalar", options =>
    {
        options.OpenApiRoutePattern = "/openapi/{documentName}.json";
    });
}



await app.RunAsync();   
