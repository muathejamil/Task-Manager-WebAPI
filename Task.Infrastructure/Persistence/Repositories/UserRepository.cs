using Microsoft.EntityFrameworkCore;
using Task.Application.Common.Persistence;
using Task.Domain.Common.Entities;

namespace Task.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskDbContext _context;
    
    public UserRepository(TaskDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> FindUserByEmailAsync(string email)
    {
        return await _context.Users
            .SingleOrDefaultAsync(u => email == u.Email);
    }

    public async System.Threading.Tasks.Task CreateUserAsync(User user)
    {
        _context.Users
            .Add(user);
        await _context.SaveChangesAsync();
    }
}