using AutoMapper;
using Task.Application.Common.Mappers.Dtos.User;

namespace Task.Application.Common.Mappers.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.Common.Entities.User, UserDto>();
    }
    
}