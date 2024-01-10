using BlogMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVC.Infra.Data.EntitiesConfiguration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(t => t.Id); 
        builder.Property(p => p.Commentary).HasMaxLength(1000); 
        builder.HasOne(e => e.Post).WithMany(e => e.Comments).HasForeignKey(e => e.PostId); 
    }
}
