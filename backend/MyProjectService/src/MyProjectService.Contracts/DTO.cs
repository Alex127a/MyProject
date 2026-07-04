using System.ComponentModel.DataAnnotations;
namespace MyProjectService.Contracts;

public record AddressDto(string Country, string City, string Street, string Building);
public record CreateDepartmentDto(
    Guid? ParentId, 

    [Required(ErrorMessage = "Название отдела обязательно.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно быть от 2 до 100 символов.")]
    string Name
);
public record GetDepartmentDto(Guid Id, Guid ParentId, string Name);
public record ListDepartmentsDto(Guid Id, string Name);
public record UpdateDepartmentDto(Guid ParentId, string Name);
public record DeleteDepartmentDto(Guid Id);

public record CreateLocationDto(
    [Required(ErrorMessage = "Название локации обязательно.")]
    [StringLength(150, ErrorMessage = "Название не должно превышать 150 символов.")]
    string Name,

    [Required(ErrorMessage = "Город обязателен.")]
    string City,

    [MaxLength(200)]
    string? AddressLine
);
public record GetLocationDto(Guid Id, Guid DepartmentId, string Name, AddressDto Address);
public record ListLocationsDto(Guid Id, string Name, AddressDto Address);
public record UpdateLocationDto(Guid DepartmentId, string Name, AddressDto Address);
public record DeleteLocationDto(Guid Id);

public record CreatePositionDto(
    [Required(ErrorMessage = "Название должности обязательно.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно быть от 2 до 100 символов.")]
    string Title,

    [Required(ErrorMessage = "Укажите департамент.")]
    Guid DepartmentId
);

public record GetPositionDto(Guid Id, Guid DepartmentId, string Title);
public record ListPositionsDto(Guid Id, string Title);
public record UpdatePositionDto(Guid DepartmentId, string Title);
public record DeletePositionDto(Guid Id);