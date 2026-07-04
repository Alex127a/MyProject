using MyProjectService.Contracts;
using MyProjectService.Domain;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace MyProjectService.Core;

public class LocationsService
{
    
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<LocationsService> _logger;
    private readonly IValidator<CreateLocationDto> _validator;

    public LocationsService(
        ILocationsRepository locationsRepository, 
        IValidator<CreateLocationDto> validator,
        ILogger<LocationsService> logger)
    {
        _locationsRepository = locationsRepository ?? throw new ArgumentNullException(nameof(locationsRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
    } 

    public async Task<Guid> Create(CreateLocationDto createLocation, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(createLocation, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
         var isUnique = await _locationsRepository.IsNameUniqueAsync(createLocation.Name, cancellationToken);
          if (!isUnique)
        {
            #pragma warning disable CA1848, CA1873 
            _logger.LogWarning("Попытка создать локацию с уже занятым именем: {Name}", createLocation.Name);
            #pragma warning restore CA1848, CA1873
            throw new LocationNameAlreadyExistsException(createLocation.Name);
        }
        
        var locationId = Guid.NewGuid();

        var location = new Location
        (
            locationId,
            Name.Create(createLocation.Name),
            Address.Create(createLocation.AddressLine!)     
        );
        await _locationsRepository.AddAsync(location, cancellationToken);
        #pragma warning disable CA1848, CA1873 
        _logger.LogInformation("Location with id {LocationId} created", locationId);
        #pragma warning restore CA1848, CA1873 
        return locationId;

        
    }  
}

