using Moq;
using StackExchange.Redis;
using System.Text.Json;

namespace education_api_test
{
    public class MockInformation
    {
        public Mock<IDatabase> MockDatabase { get; set; }
        public Mock<IConnectionMultiplexer> MockMultiplexer { get; set; }
        public JsonSerializerOptions JsonSerializerOptions { get; set; }    
    }
}
