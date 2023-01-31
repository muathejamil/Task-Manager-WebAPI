using Task.Application.Common.Mappers.Dtos.User;

namespace Task.Application.Common.Interfaces;

public interface IJwtProvider
{
    string GenerateJwtToken(UserDto user);
    
    
}