using AutoMapper;
using Task.Application.Authentication.Commands.Register;

namespace Task.Application.Common.Mappers.Profiles;

public class CommandUserProfile : Profile
{
    public CommandUserProfile()
    {
        CreateMap<RegisterCommand, Domain.Common.Entities.User>();
    }
    
}