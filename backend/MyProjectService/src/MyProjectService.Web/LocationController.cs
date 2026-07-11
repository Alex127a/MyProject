using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProjectService.Contracts;
using MyProjectService.Core;

namespace MyProjectService.Controllers;

[ApiController]
[Route("locations")] // Задает базовый эндпоинт POST /locations
public class LocationsController : ControllerBase
{
    private readonly CreateLocationHandler _handler;

    // Внедряем наш хендлер через DI контейнер
    public LocationsController(CreateLocationHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateLocationDto request, 
        CancellationToken cancellationToken)
    {
        try
        {
            // Передаем DTO в хендлер и получаем Guid созданной локации
            Guid locationId = await _handler.HandleAsync(request, cancellationToken);
            
            // Возвращаем статус 200 OK (или 201 Created) вместе с ID новой локации
            return Ok(new { Id = locationId });
        }
        catch (InvalidOperationException ex)
        {
            // Перехватываем бизнес-исключение уникальности имени
            // и возвращаем клиенту ошибку со статусом 400 Bad Request
            return BadRequest(new { Message = ex.Message });
        }
    }
}

