using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Contracts.Task;


namespace Task.Application.Common.Services.CsvReader;

public class CsvReader : ICsvReader
{
    // TODO: add logging and We can make this generic
    
    private readonly IMapper _mapper;
    
    public CsvReader(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public Task<IEnumerable<TaskDto>> ReadAsync<T>(IFormFile file)
    {
        using (var streamReader = new StreamReader(file.OpenReadStream()))
        {
            using (var csvReader = new CsvHelper.CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                IEnumerable<CreateTaskRequest> taskDtos = csvReader.GetRecords<CreateTaskRequest>();
                var result = _mapper.Map<IEnumerable<TaskDto>>(taskDtos);
                return System.Threading.Tasks.Task.FromResult(result.AsEnumerable());
            }
        }
    }
}