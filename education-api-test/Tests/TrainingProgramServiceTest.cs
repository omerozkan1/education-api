using education_api_test.Factories;
using education_application;
using education_domain;
using StackExchange.Redis;

namespace education_api_test.Tests
{
    public class TrainingProgramServiceTest
    {
        private TrainingDbContext _db;

        public TrainingProgramServiceTest()
        {
            _db = TrainingDatabaseContextFactory.GetInstance;
        }

        private TrainingProgramService GetService(TrainingDbContext db, IConnectionMultiplexer multiplexer)
        {
            var trainingProgramRepo = new TrainingProgramEfRepository(db, multiplexer);

            return new TrainingProgramService(trainingProgramRepo);
        }


        [Fact]
        public async Task Add_TrainingProgram_Success()
        {
            var service = GetService(_db, RedisFactory.Default);

            var request = new CreateTrainingProgramDto
            {
                Name = "TrainingProgramName",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1),
                Status = TrainingStatus.NotPublished
            };

            var result = await service.CreateAsync(request);

            Assert.NotNull(result);

            var trainingProgram = _db.TrainingProgram.First(t => t.Name == "TrainingProgramName");
            Assert.Equal(request.Name, trainingProgram.Name);
            Assert.Equal(request.Status, trainingProgram.Status);
            Assert.Equal(request.StartDate, trainingProgram.StartDate);
            Assert.Equal(request.EndDate, trainingProgram.EndDate);
        }

        [Fact]
        public async Task Get_TrainingPrograms_From_Redis_Cache_Success()
        {
            var service = GetService(_db, RedisFactory.GetTrainingPrograms);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(5, result.Data.Count);
        }

        [Fact]
        public async Task Get_TrainingPrograms_From_Database_Success()
        {
            var service = GetService(_db, RedisFactory.Default);

            _db.TrainingProgram.AddRange(
                new TrainingProgram { Name = "TrainingProgram", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) },
                new TrainingProgram { Name = "TrainingProgram2", Status = TrainingStatus.NotPublished, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(45) },
                new TrainingProgram { Name = "TrainingProgram3", Status = TrainingStatus.NotPublished, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2) },
                new TrainingProgram { Name = "TrainingProgram4", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(30) }
            );
            await _db.SaveChangesAsync();

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(4, result.Data.Count);
            Assert.Equal(2, result.Data.Where(tp => tp.Status == TrainingStatus.NotPublished).ToList().Count());
        }

        [Fact]
        public async Task Set_TrainingProgram_To_Redis_Cache_Success()
        {
            var service = GetService(_db, RedisFactory.SetTrainingProgram);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Data.Count);
        }
    }
}
