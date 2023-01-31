using AutoMapper;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Contracts.Task;

namespace Task.Api.Common.Mappers.Profiles;

public class UpsertTaskProfile : Profile
{
    public UpsertTaskProfile()
    {
        CreateMap<UpsertTaskRequest, TaskDto>();
        
    }
    
}