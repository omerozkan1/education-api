using education_api_test.Factories;
using education_application;
using education_domain;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace education_api_test.Tests
{
    public class TrainingServiceTest
    {
        private TrainingDbContext _db;

        public TrainingServiceTest()
        {
            _db = TrainingDatabaseContextFactory.GetInstance;
        }


        private TrainingService GetService(TrainingDbContext db, IConnectionMultiplexer multiplexer)
        {
            var trainingRepo = new TrainingEfRepository(db, multiplexer);

            return new TrainingService(trainingRepo);
        }


        [Fact]
        public async Task Add_Training_Success()
        {
            var service = GetService(_db, RedisFactory.Default);

            var request = new CreateTrainingDto
            {
                Name = "TrainingName",
                Description = "TrainingDescription",
                Link = "TrainingLink",
                TrainingProgramId = 1
            };

            var result = await service.CreateAsync(request);

            Assert.NotNull(result);

            var training = _db.Training.First(t => t.Name == "TrainingName");
            Assert.Equal(request.Name, training.Name);
            Assert.Equal(request.Description, training.Description);
            Assert.Equal(request.Link, training.Link);
            Assert.Equal(request.TrainingProgramId, training.TrainingProgramId);
        }



        [Fact]
        public async Task Get_Trainings_From_Redis_Cache_Success()
        {
            var service = GetService(_db, RedisFactory.GetTrainings);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, result.Data.Count);
        }


        [Fact]
        public async Task Get_Trainings_From_Database_Success()
        {
            var service = GetService(_db, RedisFactory.Default);

            _db.Training.AddRange(
                new Training { Name = "TrainingName", Link = "Link", Description = "TrainingDescription", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(30) } },
                new Training { Name = "TrainingName2", Link = "Link2", Description = "TrainingDescription2", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName2", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(45) } },
                new Training { Name = "TrainingName3", Link = "Link3", Description = "TrainingDescription3", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName3", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) } },
                new Training { Name = "TrainingName4", Link = "Link4", Description = "TrainingDescription4", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName4", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(2) } }
            );

            await _db.SaveChangesAsync();

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(4, result.Data.Count);
        }


        [Fact]
        public async Task Set_Training_To_Redis_Cache_Success()
        {
            var service = GetService(_db, RedisFactory.SetTraining);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, result.Data.Count);
        }
    }
}