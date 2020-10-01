using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonstersIncData;
using MonstersIncLogic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonstersInc.IntimidatorIntimidationsCache
{
    public class IntimidatorIntimidationsCacheService : BackgroundService
    {
        private readonly IntimidatorIntimidationsCacheChannel _intimidatorIntimidationsCacheChannel;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDistributedCache _distributedCache;
        public IntimidatorIntimidationsCacheService(IntimidatorIntimidationsCacheChannel _intimidatorIntimidationsCacheChannel
                                                   , IServiceScopeFactory _serviceScopeFactory
                                                   , IDistributedCache _distributedCache)
        {
            this._intimidatorIntimidationsCacheChannel = _intimidatorIntimidationsCacheChannel ?? throw new ArgumentNullException(nameof(_intimidatorIntimidationsCacheChannel));
            this._serviceScopeFactory = _serviceScopeFactory ?? throw new ArgumentNullException(nameof(_serviceScopeFactory));
            this._distributedCache = _distributedCache ?? throw new ArgumentNullException(nameof(_distributedCache));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await foreach (var id in _intimidatorIntimidationsCacheChannel.ReadAllAsync())
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _doorRepository = scope.ServiceProvider.GetService<IDoorRepository>();
                    var _intimidatorWorkdayRepository = scope.ServiceProvider.GetService<IintimidatorWorkdayRepository>();
                    var _intimidatorRepository = scope.ServiceProvider.GetService<IintimidatorRepository>();

                    var results = await new IntimidatorIntimidationsSummery(_doorRepository, _intimidatorWorkdayRepository
                                                                          , _intimidatorRepository, _distributedCache, id).ReadAndCacheAsync();
                }
            }
        }
    }
}
