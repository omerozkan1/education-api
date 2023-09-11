using education_domain;
using education_infrastructure.StaticValues;
using Moq;
using StackExchange.Redis;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace education_api_test.Factories
{
    public static class RedisFactory
    {
        public static IConnectionMultiplexer Default
        {
            get
            {
                var mockInfo = GetMockInfo();

                return mockInfo.MockMultiplexer.Object;
            }
        }

        public static IConnectionMultiplexer GetTrainings
        {
            get
            {
                var mockInfo = GetMockInfo();

                var trainings = new List<Training>() {
                    new Training { Name = "TrainingName", Link = "Link", Description = "TrainingDescription", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(30) } },
                    new Training { Name = "TrainingName2", Link = "Link2", Description = "TrainingDescription2", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName2", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(45) } },
                    new Training { Name = "TrainingName3", Link = "Link3", Description = "TrainingDescription3", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName3", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) } }
                };

                mockInfo.MockDatabase
                    .Setup(h => h.StringGetAsync(It.IsAny<RedisKey>(), CommandFlags.None))
                    .ReturnsAsync(System.Text.Json.JsonSerializer.Serialize<List<Training>>(trainings, mockInfo.JsonSerializerOptions));

                return mockInfo.MockMultiplexer.Object;
            }
        }


        public static IConnectionMultiplexer SetTraining
        {
            get
            {
                var mockInfo = GetMockInfo();

                var trainings = new List<Training>() {
                    new Training { Name = "TrainingName", Link = "Link", Description = "TrainingDescription", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(30) } },
                    new Training { Name = "TrainingName2", Link = "Link2", Description = "TrainingDescription2", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName2", Status = TrainingStatus.NotPublished, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(45) } },
                    new Training { Name = "TrainingName3", Link = "Link3", Description = "TrainingDescription3", TrainingProgram = new TrainingProgram { Name = "TrainingProgramName3", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) } }
                };

                mockInfo.MockDatabase.Setup(h => h.StringSetAsync(ApplicationConsts.TRAINING, 
                    System.Text.Json.JsonSerializer.Serialize<List<Training>>(trainings, mockInfo.JsonSerializerOptions), 
                    null, 
                    When.NotExists,
                    CommandFlags.None))
                    .ReturnsAsync(() => true);

                mockInfo.MockDatabase
                    .Setup(h => h.StringGetAsync(ApplicationConsts.TRAINING, CommandFlags.None))
                    .ReturnsAsync(System.Text.Json.JsonSerializer.Serialize<List<Training>>(trainings, mockInfo.JsonSerializerOptions));

                return mockInfo.MockMultiplexer.Object;
            }
        }


        public static IConnectionMultiplexer GetTrainingPrograms
        {
            get
            {
                var mockInfo = GetMockInfo();

                var trainingPrograms = new List<TrainingProgram>() {
                     new TrainingProgram { Name = "TrainingProgram", Status = TrainingStatus.NotPublished, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5), },
                     new TrainingProgram { Name = "TrainingProgram2", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(50) },
                     new TrainingProgram { Name = "TrainingProgram3", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(10) },
                     new TrainingProgram { Name = "TrainingProgram4", Status = TrainingStatus.NotPublished, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(15) },
                     new TrainingProgram { Name = "TrainingProgram5", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) }
                };

                mockInfo.MockDatabase
                    .Setup(h => h.StringGetAsync(It.IsAny<RedisKey>(), CommandFlags.None))
                    .ReturnsAsync(System.Text.Json.JsonSerializer.Serialize<List<TrainingProgram>>(trainingPrograms, mockInfo.JsonSerializerOptions));

                return mockInfo.MockMultiplexer.Object;
            }
        }


        public static IConnectionMultiplexer SetTrainingProgram
        {
            get
            {
                var mockInfo = GetMockInfo();

                var trainingPrograms = new List<TrainingProgram>() {
                     new TrainingProgram { Name = "TrainingProgram", Status = TrainingStatus.NotPublished, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5), },
                     new TrainingProgram { Name = "TrainingProgram2", Status = TrainingStatus.Published, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(50) }
                };

                mockInfo.MockDatabase.Setup(h => h.StringSetAsync(ApplicationConsts.TRAININGPROGRAMS,
                    System.Text.Json.JsonSerializer.Serialize<List<TrainingProgram>>(trainingPrograms, mockInfo.JsonSerializerOptions),
                    null,
                    When.NotExists,
                    CommandFlags.None))
                    .ReturnsAsync(() => true);

                mockInfo.MockDatabase
                    .Setup(h => h.StringGetAsync(ApplicationConsts.TRAININGPROGRAMS, CommandFlags.None))
                    .ReturnsAsync(System.Text.Json.JsonSerializer.Serialize<List<TrainingProgram>>(trainingPrograms, mockInfo.JsonSerializerOptions));

                return mockInfo.MockMultiplexer.Object;
            }
        }



        private static MockInformation GetMockInfo()
        {
            var model = new MockInformation();
            var mockMultiplexer = new Mock<IConnectionMultiplexer>();

            mockMultiplexer.Setup(_ => _.IsConnected).Returns(false);

            var mockDatabase = new Mock<IDatabase>();

            mockMultiplexer
                .Setup(_ => _.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(mockDatabase.Object);

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            model.MockMultiplexer = mockMultiplexer;
            model.MockDatabase = mockDatabase;
            model.JsonSerializerOptions = options;

            return model;
        }
    }
}
