namespace MyProjectService.Web;

using Microsoft.AspNetCore.Mvc;
using MyProjectService.Contracts;

[ApiController]
[Route("api/positions")]
public class PositionController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePositionDto createPosition, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        Guid newPositionId = Guid.NewGuid();
        return CreatedAtAction(nameof(GetById), new { positionId = newPositionId }, newPositionId);
    }

    [HttpGet("{positionId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid positionId, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (positionId == Guid.Empty) 
        {
            return NotFound(); 
        }

        var positionResponse = new GetPositionDto(positionId, Guid.NewGuid(), "имя");
        
        return Ok(positionResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        
        var positions = new List<ListPositionsDto>
        {
            new(Guid.NewGuid(), "Должность 1"),
            new(Guid.NewGuid(), "Должность 2")
        };
        
        return Ok(positions);
    }

    [HttpPut("{positionId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid positionId, [FromBody] UpdatePositionDto updatePosition, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (positionId == Guid.Empty) 
        {
            return NotFound();
        }
        
        return NoContent(); 
    }
    [HttpDelete("{positionId:guid}")]
    
    public async Task<IActionResult> Delete([FromRoute] Guid positionId, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (positionId == Guid.Empty) 
        {
            return NotFound();
        }
    
        return NoContent();
    }
}
