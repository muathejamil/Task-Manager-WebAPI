using Task.Domain.Common.Entities;

namespace Task.Application.Common.Persistence;

public interface IUserRepository
{
    Task<User?> FindUserByEmailAsync(string email);
    System.Threading.Tasks.Task CreateUserAsync(User user);
}