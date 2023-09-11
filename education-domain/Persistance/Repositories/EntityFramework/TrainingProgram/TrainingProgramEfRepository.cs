using education_infrastructure.StaticValues;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace education_domain
{
    public class TrainingProgramEfRepository : ITrainingProgramEfRepository
    {
        private readonly TrainingDbContext _dbContext;
        private readonly IConnectionMultiplexer _redisConnection;
        public TrainingProgramEfRepository(TrainingDbContext dbContext, IConnectionMultiplexer redisConnection)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _redisConnection = redisConnection ?? throw new ArgumentNullException();
        }
        public async Task<bool> CreateAsync(TrainingProgram trainingProgram)
        {
            await _dbContext.AddAsync(trainingProgram);
            var result = await _dbContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<List<TrainingProgram>> GetAllAsync()
        {
            var cacheKey = ApplicationConsts.TRAININGPROGRAMS;
            var redisValue = await _redisConnection.GetDatabase().StringGetAsync(cacheKey);

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            if (redisValue.HasValue)
                return JsonSerializer.Deserialize<List<TrainingProgram>>(redisValue, options);

            var response = await _dbContext.TrainingProgram.AsNoTracking().Include(t => t.Trainings).ToListAsync();

            if (response.Any())
            {
                await _redisConnection.GetDatabase().StringSetAsync(cacheKey, JsonSerializer.Serialize<List<TrainingProgram>>(response, options), new TimeSpan(0, 30, 0));
            }

            return response;
        }
    }
}
