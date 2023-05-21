using Utils.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Utils.Persistence.EntityMappings
{
    public class TestMapping : IEntityTypeConfiguration<Test>
    {

        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Tests");
        }
    }
}
