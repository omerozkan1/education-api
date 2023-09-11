using education_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace education_api_test.Factories
{
    internal static class TrainingDatabaseContextFactory
    {
        public static TrainingDbContext GetInstance
        {
            get
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                var options = new DbContextOptionsBuilder<TrainingDbContext>()
                             .UseInMemoryDatabase(Guid.NewGuid().ToString())
                             .UseInternalServiceProvider(serviceProvider)
                             .EnableSensitiveDataLogging()
                             .Options;

                var db = new TrainingDbContext(options);
                db.Database.EnsureCreated();

                return db;
            }
        }
    }
}
