using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace education_domain.Persistance.Context.EntityFramework
{
    public class TrainingDbContextFactory : IDesignTimeDbContextFactory<TrainingDbContext>
    {
        public TrainingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TrainingDbContext>();
            optionsBuilder.UseNpgsql("User ID=postgres; Password=123456; Server=localhost; Port=5432; Database=postgres; Integrated Security=true; Pooling=true");

            return new TrainingDbContext(optionsBuilder.Options);
        }
    }
}
