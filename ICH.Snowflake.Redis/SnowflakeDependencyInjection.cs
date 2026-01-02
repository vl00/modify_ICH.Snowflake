using ICH.Snowflake;
using ICH.Snowflake.Redis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SnowflakeDependencyInjection
    {
        public static IServiceCollection AddSnowflakeWithRedis(this IServiceCollection service, Action<RedisOption> option = null)
        {
            if (option != null) service.Configure(option);
            if (service.Any(_ => _.ServiceType == typeof(MarkerService))) return service;
            service.AddSingleton<MarkerService>();
            service.TryAddSingleton<ISnowflakeIdMaker, SnowflakeIdMaker>();
            service.AddSingleton<IRedisClient, RedisClient>();
            service.TryAddSingleton<IDistributedSupport, DistributedSupportWithRedis>();
            service.AddHostedService<SnowflakeBackgroundServices>();
            return service;
        }

        class MarkerService { }
    }
}