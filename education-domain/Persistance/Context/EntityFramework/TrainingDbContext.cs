using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace education_domain
{
    public class TrainingDbContext : DbContext
    {
      
        public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<TrainingProgram> TrainingProgram { get; set; }
    }
}
