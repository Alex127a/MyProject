using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyProjectService.Core;
using MyProjectService.Domain;
using Npgsql;

namespace MyProjectService.Infrastructure.Repositories;

public class DapperLocationsRepository : ILocationsRepository
{
    private readonly string _connectionString;
    private readonly ILogger<DapperLocationsRepository> _logger;

    public DapperLocationsRepository(string connectionString, ILogger<DapperLocationsRepository> logger)
    {
        _connectionString = connectionString ?? throw new InvalidOperationException("Строка подключения не найдена.");
        _logger = logger;
    }

    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        
        
        const string sql = "SELECT NOT EXISTS(SELECT 1 FROM locations WHERE name = @Name);";
        
        var command = new CommandDefinition(sql, new { Name = name }, cancellationToken: cancellationToken);
        return await connection.ExecuteScalarAsync<bool>(command);
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
{
    using var connection = new NpgsqlConnection(_connectionString);
    
    
    const string sql = @"
        INSERT INTO locations (id, name, address, created, updated)  
        VALUES (@Id, @Name, @Address, @CreatedAt, @UpdatedAt);";

    try
    {
        var command = new CommandDefinition(
            sql, 
            new 
            { 
                Id = location.Id, 
                Name = location.Name.Value,       
                Address = location.Address.Value,
                CreatedAt = location.CreatedAt,
                UpdatedAt = location.UpdatedAt
            }, 
            cancellationToken: cancellationToken
        );

        await connection.ExecuteAsync(command);
        return location.Id;
    }
    catch (Exception ex)
    {
        
        _logger.LogError(ex, "Dapper: Ошибка сохранения локации {LocationId}", location.Id);
        
        throw new InvalidOperationException($"Не удалось сохранить локацию {location.Id} в базу данных через Dapper.", ex);
    }
}


    
    public Task<Guid> SaveAsync(Guid id, Location location, CancellationToken cancellationToken) 
        => throw new NotSupportedException("Метод SaveAsync пока не поддерживается этой реализацией.");

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken) 
        => throw new NotSupportedException("Метод DeleteAsync пока не поддерживается этой реализацией.");

    public Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => throw new NotSupportedException("Метод GetByIdAsync пока не поддерживается этой реализацией.");
}
