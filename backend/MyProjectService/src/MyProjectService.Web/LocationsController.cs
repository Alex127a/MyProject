namespace MyProjectService.Web;

using Microsoft.AspNetCore.Mvc;
using MyProjectService.Contracts;

[ApiController]
[Route("api/locations")]
public class LocationController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationDto createLocation, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        Guid newLocationId = Guid.NewGuid();
        return CreatedAtAction(nameof(GetById), new { locationId = newLocationId }, newLocationId);
    }

    [HttpGet("{locationId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid locationId, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (locationId == Guid.Empty) 
        {
            return NotFound();
        }
        var mockAddress = new AddressDto("Страна", "Город", "Улица", "Номер дома");
        var locationResponse = new GetLocationDto(locationId, Guid.NewGuid(), "Sample Location", mockAddress);
        return Ok(locationResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var mockAddress = new AddressDto("Страна", "Город", "Улица", "Номер дома");

        var locations = new List<ListLocationsDto>
        {
            new(Guid.NewGuid(), "name location 1", mockAddress),
            new(Guid.NewGuid(), "name location 2", mockAddress)
        };
        return Ok(locations);
    }

    [HttpPut("{locationId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid locationId, [FromBody] UpdateLocationDto updateLocation, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (locationId == Guid.Empty) return NotFound();
        return NoContent();
    }

    [HttpDelete("{locationId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid locationId, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (locationId == Guid.Empty) return NotFound();
        return NoContent();
    }
}
