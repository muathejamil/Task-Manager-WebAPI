namespace Task.Application.Common.Mappers.Dtos.User;

public record UserDto(
    Guid Id, 
    string FirstName, 
    string LastName, 
    string Email);