using DQueue.Infrastructure;
using StackExchange.Redis;
using System;

namespace RedisPubSub
{
    public class RedisSubscriber
    {
        static ConnectionMultiplexer _redisConnectionFactory;

        static RedisSubscriber()
        {
            var redisConnectionString = ConfigSource.GetConnection("Redis_Connection");
            var resisConfiguration = ConfigurationOptions.Parse(redisConnectionString);
            _redisConnectionFactory = ConnectionMultiplexer.Connect(resisConfiguration);
        }

        protected ISubscriber GetSubscriber()
        {
            return _redisConnectionFactory.GetSubscriber();
        }

        public void Publish(string channel, string message)
        {
            GetSubscriber().Publish(channel, message);
        }

        public void Subscribe(string channel, Action<string> callback)
        {
            GetSubscriber().Subscribe(channel, (cnl, val) =>
            {
                callback(val);
            });
        }
    }
}
