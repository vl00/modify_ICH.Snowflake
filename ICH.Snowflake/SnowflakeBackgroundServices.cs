using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ICH.Snowflake
{
    public class SnowflakeBackgroundServices : BackgroundService
    {
        private readonly ISnowflakeIdMaker _idMaker;
        private readonly IDistributedSupport _distributed;
        private readonly SnowflakeOption option;
        private readonly ILogger _logger;

        public SnowflakeBackgroundServices(ISnowflakeIdMaker idMaker, IDistributedSupport distributed, IOptions<SnowflakeOption> options, ILogger<SnowflakeBackgroundServices> logger)
        {
            _idMaker = idMaker;
            option = options.Value;
            _distributed = distributed;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                while (true)
                {
                    // 定时刷新机器id的存活状态
                    try { await _distributed.RefreshAlive(); }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "exec '_distributed.RefreshAlive' error.");
                    }

                    await Task.Delay(option.RefreshAliveInterval.Add(TimeSpan.FromMinutes(1)), stoppingToken);
                }

            }
        }
    }
}