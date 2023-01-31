using Microsoft.EntityFrameworkCore;

namespace Task.Infrastructure.Persistence;

public class TaskDbContext : DbContext
{
    
    public TaskDbContext (DbContextOptions<TaskDbContext> options) 
        : base(options)
    {
        
    }
    
    public DbSet<Domain.Task.Task> Tasks { get; set; } = null!;
    public DbSet<Domain.Common.Entities.User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Task.Task>()
            .HasIndex(t => t.Title)
            .IsUnique();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
        base.OnModelCreating(modelBuilder); 
    }
}