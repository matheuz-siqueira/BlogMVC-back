using BlogMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVC.Infra.Data.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(t => t.Id); 
        builder.Property(p => p.Name).HasMaxLength(30).IsRequired(); 
        builder.Property(p => p.Surname).HasMaxLength(30).IsRequired();  
        builder.Property(p => p.Email).HasMaxLength(250).IsRequired(); 
        builder.Property(p => p.Password).IsRequired(); 
    }
}
