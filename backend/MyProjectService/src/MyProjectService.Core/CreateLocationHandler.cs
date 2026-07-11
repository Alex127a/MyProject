using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MyProjectService.Contracts;
using MyProjectService.Domain;

namespace MyProjectService.Core;

public class CreateLocationHandler
{
    private readonly ILocationsRepository _repository;
    private readonly IValidator<CreateLocationDto> _validator;

    public CreateLocationHandler(ILocationsRepository repository, IValidator<CreateLocationDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Guid> HandleAsync(CreateLocationDto createLocation, CancellationToken cancellationToken)
    {
        
        var validationResult = await _validator.ValidateAsync(createLocation, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        
        var isNameUnique = await _repository.IsNameUniqueAsync(createLocation.Name, cancellationToken);
        if (!isNameUnique)
        {
            throw new InvalidOperationException($"Локация с именем '{createLocation.Name}' уже существует.");
        }

       
        var locationId = Guid.NewGuid();
        
        
        var domainName = Name.Create(createLocation.Name);
        
        
        var fullAddressString = $"{createLocation.City}, {createLocation.AddressLine}";
        var domainAddress = Address.Create(fullAddressString);

       
        var location = new Location(locationId, domainName, domainAddress);

        
        await _repository.AddAsync(location, cancellationToken);

        
        return locationId;
    }
}

