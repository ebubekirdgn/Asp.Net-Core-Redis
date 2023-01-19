using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisService
    {
        private readonly IDatabase _cache;
        private readonly IConnectionMultiplexer _redisCon;

        public RedisService(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase();
        }
        public IDatabase GetDb(int db) => _redisCon.GetDatabase(db);
    }
}