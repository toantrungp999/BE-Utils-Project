using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Utils.Domain.Entities;
using System.Reflection.Emit;

namespace Utils.Persistence.EntityMappings
{
    public class PostCategoryMapping : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder.HasKey(x => new { x.PostId, x.CategoryId });
            builder.ToTable("PostCategories");
        }
    }
}
