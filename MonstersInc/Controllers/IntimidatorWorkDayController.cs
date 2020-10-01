using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using MonstersInc.Auth;
using MonstersIncLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc.Controllers
{
    [Authorize]
    [Route("api/intimidators/workday")]
    [ApiController]
    public class IntimidatorWorkDayController : ControllerBase
    {
        private readonly IintimidatorWorkdayRepository _intimidatorWorkdayRepository;
        private readonly IDoorRepository _doorRepository;
        private readonly IintimidatorRepository _intimidatorRepository;
        private readonly IDistributedCache _distributedCache;


        public IntimidatorWorkDayController(IDoorRepository _doorRepository
                                          , IintimidatorWorkdayRepository _intimidatorWorkdayRepository
                                          , IintimidatorRepository _intimidatorRepository
                                          , IDistributedCache _distributedCache
            )
        {
            this._doorRepository = _doorRepository ?? throw new ArgumentNullException(nameof(_doorRepository)); ;
            this._intimidatorWorkdayRepository = _intimidatorWorkdayRepository ?? throw new ArgumentNullException(nameof(_intimidatorWorkdayRepository)); ;
            this._intimidatorRepository = _intimidatorRepository ?? throw new ArgumentNullException(nameof(_intimidatorRepository));
            this._distributedCache = _distributedCache ?? throw new ArgumentNullException(nameof(_distributedCache));
        }
        [HttpGet]
        public async Task<IActionResult> GetDailyWork(DateTime? start, DateTime? end, bool? dailyGoalAccomplishing = null)
        {

            var intimidatorSummery = new IntimidatorIntimidationsSummery(_doorRepository, _intimidatorWorkdayRepository
                                                            , _intimidatorRepository, _distributedCache, await AuthHelper.GetIntimidatorId(User, _intimidatorRepository));

            var results = default(List<IntimidatorWorkdayDto>);

            results = await intimidatorSummery.ReadFromCacheAsync();

            if (results == null)
                results = await intimidatorSummery.ReadAndCacheAsync();

            if (start != null && end != null)
                results = results.Where(d => d.Day >= start && d.Day <= end).ToList();
            else if (start != null)
                results = results.Where(d => d.Day >= start).ToList();
            else if (end != null)
                results = results.Where(d => d.Day <= end).ToList();

            if (dailyGoalAccomplishing != null)
                results = results.Where(d => d.GoalAccomplished == dailyGoalAccomplishing.Value).ToList();

            return Ok(results);
        }
    }
}
