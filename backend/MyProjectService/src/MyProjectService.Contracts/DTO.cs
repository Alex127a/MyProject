namespace MyProjectService.Contracts;

public record AddressDto(string Country, string City, string Street, string Building);
public record CreateDepartmentDto(Guid ParentId, string Name);
public record GetDepartmentDto(Guid Id, Guid ParentId, string Name);
public record ListDepartmentsDto(Guid Id, string Name);
public record UpdateDepartmentDto(Guid ParentId, string Name);
public record DeleteDepartmentDto(Guid Id);

public record CreateLocationDto(Guid DepartmentId, string Name, AddressDto Address);
public record GetLocationDto(Guid Id, Guid DepartmentId, string Name, AddressDto Address);
public record ListLocationsDto(Guid Id, string Name, AddressDto Address);
public record UpdateLocationDto(Guid DepartmentId, string Name, AddressDto Address);
public record DeleteLocationDto(Guid Id);

public record CreatePositionDto(Guid LocationId, string Name);
public record GetPositionDto(Guid Id, Guid LocationId, string Name);
public record ListPositionsDto(Guid Id, string Name);
public record UpdatePositionDto(Guid LocationId, string Name);
public record DeletePositionDto(Guid Id);