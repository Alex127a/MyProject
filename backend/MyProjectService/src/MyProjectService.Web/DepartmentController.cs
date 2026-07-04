namespace MyProjectService.Web;
using Microsoft.AspNetCore.Mvc;
using MyProjectService.Contracts;

[ApiController]
[Route("api/departments")]

public class DepartmentController : ControllerBase
{
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateDepartmentDto createDepartment, CancellationToken cancellationToken)
{
  Guid newDepartmentId = Guid.NewGuid();
  return CreatedAtAction(nameof(GetById), new { departmentId = newDepartmentId }, newDepartmentId);
    
}
[HttpGet("{departmentId:guid}")]
public async Task<IActionResult> GetById([FromRoute] Guid departmentId, CancellationToken cancellationToken)
{
      if (departmentId == Guid.Empty) 
        {
            return NotFound();
        }
        var departmentResponse = new GetDepartmentDto(departmentId, Guid.NewGuid(), "Sample Department");
        return Ok(departmentResponse);
}

[HttpGet]
public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
{
   var departments = new List<ListDepartmentsDto>
        {
            new(Guid.NewGuid(), "Sample Department 1"),
            new(Guid.NewGuid(), "Sample Department 2")
        };
        return Ok(departments);
       
}

[HttpPut("{departmentId:guid}")]
public async Task<IActionResult> Update([FromRoute] Guid departmentId, [FromBody] UpdateDepartmentDto updateDepartment, CancellationToken cancellationToken)

{
  if (departmentId == Guid.Empty) 
        {
            return NotFound();
        }

        return NoContent();
}

[HttpDelete("{departmentId:guid}")]
public async Task<IActionResult> Delete([FromRoute] Guid departmentId, CancellationToken cancellationToken)
{
  if (departmentId == Guid.Empty) 
        {
            return NotFound();
        }
 
        return NoContent(); 
}
}
