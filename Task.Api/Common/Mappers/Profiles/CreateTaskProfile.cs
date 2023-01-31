using AutoMapper;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Contracts.Task;

namespace Task.Api.Common.Mappers.Profiles;

public class CreateTaskProfile : Profile
{
    public CreateTaskProfile()
    {
        CreateMap<CreateTaskRequest, TaskDto>()
            .ConstructUsing((src, context) => new TaskDto(Guid.NewGuid(), src.Title, src.StartDate, src.EndDate))
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()));
            
    }
    
}