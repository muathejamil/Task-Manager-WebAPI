using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Task.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Task.Task> builder)
    {
        ConfigureTaskTable(builder);
    }

    private void ConfigureTaskTable(EntityTypeBuilder<Domain.Task.Task> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();
        
        ConfigureTaskProperty(builder, t => t.Title, "Title", 100, true);
        ConfigureTaskProperty(builder, t => t.StartDate, "StartDate", 0, true);
        ConfigureTaskProperty(builder, t => t.EndDate, "EndDate", 0, true);
        
    }
    
    private void ConfigureTaskProperty(
        EntityTypeBuilder<Domain.Task.Task> builder,
        Expression<Func<Domain.Task.Task, object>> propertyExpression,
        string columnName, int maxLength, bool isRequired)
    {
        builder.Property(propertyExpression)
            .HasColumnName(columnName);
        if (maxLength > 0)
        {
            builder.Property(propertyExpression)
                .HasMaxLength(maxLength);
        }
        if (isRequired)
        {
            builder.Property(propertyExpression)
                .IsRequired();
        }
    }
}