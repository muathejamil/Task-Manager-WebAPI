using AutoMapper;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Application.Task.Commands.Update;
using Task.Contracts.Common;
using Task.Contracts.Task;
using TaskResponse = Task.Application.Task.Common.TaskResponse;

namespace Task.Application.Common.Mappers.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Domain.Task.Task, TaskDto>();
        CreateMap<Page<Domain.Task.Task>, Page<TaskDto>>();
        CreateMap<TaskDto, Domain.Task.Task>();
        CreateMap<Domain.Task.Task, TaskResponse>();
        CreateMap<CreateTaskRequest, TaskDto>()
            .ConstructUsing((src, context) => new TaskDto(Guid.NewGuid(), src.Title, src.StartDate, src.EndDate))
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<UpdateCommand, Domain.Task.Task>();
    }

}