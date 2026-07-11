using MyProjectService.Domain;

namespace MyProjectService.Core;

public interface ILocationsRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);
    Task<Guid> SaveAsync(Guid id, Location location, CancellationToken cancellationToken);
    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
}