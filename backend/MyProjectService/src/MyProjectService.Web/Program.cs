using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using MyProjectService.Infrastructure.Postgres;
using Scalar.AspNetCore;
using DotNetEnv;
using Npgsql;


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

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();

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
