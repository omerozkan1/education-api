using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace education_domain.Persistance.Configurations
{
    public class TrainingProgramEntityTypeConfiguration : IEntityTypeConfiguration<TrainingProgram>
    {
        public void Configure(EntityTypeBuilder<TrainingProgram> builder)
        {
            builder.Property(e => e.Name);
            builder.Property(e => e.StartDate);
            builder.Property(e => e.EndDate);
            builder.Property(e => e.Status);
        }
    }
}
