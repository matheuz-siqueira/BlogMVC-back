using BlogMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVC.Infra.Data.EntitiesConfiguration;

public class TagsConfiguration : IEntityTypeConfiguration<Tags>
{
    public void Configure(EntityTypeBuilder<Tags> builder)
    {
        builder.HasKey(t => t.Id); 
        builder.Property(p => p.Tag).HasMaxLength(12).IsRequired(); 
        builder.HasOne(p => p.Post).WithMany(e => e.Tags).HasForeignKey(e => e.PostId); 
    }
}
