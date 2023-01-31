using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task.Domain.Common.Entities;

namespace Task.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);
    }
    
    private void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();
    
        ConfigureUserProperty(builder, u => u.Email, "Email", 100, true);
        ConfigureUserProperty(builder, u => u.FirstName, "FirstName", 100, true);
        ConfigureUserProperty(builder, u => u.LastName, "LastName", 100, true);
        ConfigureUserProperty(builder, u => u.Password, "Password", 0, true);
    }
    
    private void ConfigureUserProperty(
        EntityTypeBuilder<User> builder,
        Expression<Func<User, object>> propertyExpression,
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