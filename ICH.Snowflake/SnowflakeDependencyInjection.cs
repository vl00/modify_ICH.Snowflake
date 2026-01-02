using ICH.Snowflake;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SnowflakeDependencyInjection
    {
        public static IServiceCollection AddSnowflake(this IServiceCollection service, Action<SnowflakeOption> option = null)
        {
            if (option != null) service.Configure(option);
            service.TryAddSingleton<ISnowflakeIdMaker, SnowflakeIdMaker>();
            return service;
        }
    }
}