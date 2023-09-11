using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace education_domain.Persistance.Configurations
{
    public class TrainingEntityTypeConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.Property(e => e.Name);
            builder.Property(e => e.Description);
            builder.Property(e => e.Link);
        }
    } 
}
