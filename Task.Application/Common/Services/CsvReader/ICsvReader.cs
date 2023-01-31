using Microsoft.AspNetCore.Http;
using Task.Application.Common.Mappers.Dtos.Task;

namespace Task.Application.Common.Services.CsvReader;

public interface ICsvReader
{
    Task<IEnumerable<TaskDto>> ReadAsync<T>(IFormFile file);
}