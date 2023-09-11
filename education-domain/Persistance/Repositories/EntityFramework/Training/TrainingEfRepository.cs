using education_infrastructure.StaticValues;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace education_domain
{
    public class TrainingEfRepository : ITrainingEfRepository
    {
        private readonly TrainingDbContext _dbContext;
        private readonly IConnectionMultiplexer _redisConnection;
        public TrainingEfRepository(TrainingDbContext dbContext, IConnectionMultiplexer redisConnection)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _redisConnection = redisConnection ?? throw new ArgumentNullException();
        }
        public async Task<bool> CreateAsync(Training training)
        {
            await _dbContext.AddAsync(training);
            var result = await _dbContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<List<Training>> GetAllAsync()
        {
            var cacheKey = ApplicationConsts.TRAINING;
            var redisValue = await _redisConnection.GetDatabase().StringGetAsync(cacheKey);

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            if (redisValue.HasValue)
                return JsonSerializer.Deserialize<List<Training>>(redisValue, options);

            var response = await _dbContext.Training.AsNoTracking().Include(t => t.TrainingProgram).ToListAsync();

            if(response.Any())
            {
                await _redisConnection.GetDatabase().StringSetAsync(cacheKey, JsonSerializer.Serialize<List<Training>>(response, options), new TimeSpan(0, 30, 0));
            }

            return response;
        }
    }
}
