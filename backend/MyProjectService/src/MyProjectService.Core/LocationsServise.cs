using MyProjectService.Contracts;
using MyProjectService.Domain;

namespace MyProjectService.Core;




public static class LocationsService
{
    public static async Task<Location> Create(CreateLocationDto createLocation, CancellationToken cancellationToken)
    {
        var locationId = Guid.NewGuid();
        {
      var location = new Location
        {
            Id = Guid.NewGuid(),
            Name = createLocation.Name,
            Address = createLocation.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await Task.CompletedTask; // Simulate async operation

        return location; 

    }
   
    //public async Task<IActionResult> Update( Guid locationId, [FromBody] UpdateLocationDto updateLocation, CancellationToken cancellationToken)
    
    //public async Task<IActionResult> Delete([FromRoute] Guid locationId, CancellationToken cancellationToken)
    
}

