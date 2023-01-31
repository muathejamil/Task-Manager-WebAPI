using AutoMapper;
using Task.Application.Authentication.Commands.Register;
using Task.Application.Authentication.Queries;
using Task.Contracts.Authentication;

namespace Task.Api.Common.Mappers.Profiles;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<LoginRequest, LoginQuery>();

    }
}