using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyProjectService.Domain;
using MyProjectService.Core;

namespace MyProjectService.Infrastructure.Postgres;

public class EfLocationsRepository : ILocationsRepository
{
    private readonly AppDBContext _context;
    private readonly ILogger<EfLocationsRepository> _logger;

    public EfLocationsRepository(AppDBContext context, ILogger<EfLocationsRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
       
        var exists = await _context.Locations.AnyAsync(l => l.Name.Value == name, cancellationToken);
        return !exists;
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Locations.AddAsync(location, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return location.Id;
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex, "EF Core: Ошибка сохранения локации {LocationId}", location.Id);
             
             throw new InvalidOperationException($"Не удалось сохранить локацию {location.Id} в базу данных через EF Core.", ex);
        }
    }

    
    
    public Task<Guid> SaveAsync(Guid id, Location location, CancellationToken cancellationToken) 
    => throw new NotSupportedException("Метод SaveAsync пока не поддерживается этой реализацией.");

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken) 
    => throw new NotSupportedException("Метод DeleteAsync пока не поддерживается этой реализацией.");

    public Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
    => throw new NotSupportedException("Метод GetByIdAsync пока не поддерживается этой реализацией.");


}
