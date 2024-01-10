using BlogMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVC.Infra.Data.EntitiesConfiguration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(t => t.Id); 
        builder.Property(p => p.Title).HasMaxLength(120).IsRequired();
        builder.Property(p => p.Subtitle).HasMaxLength(120); 
        builder.Property(p => p.Content).HasMaxLength(2000).IsRequired();  
    }
}
