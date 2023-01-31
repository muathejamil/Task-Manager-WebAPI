using Task.Application.Common.Mappers.Dtos.User;

namespace Task.Application.Authentication.Common;

public record AuthenticationResponse(
    UserDto? User,
    string Token);
