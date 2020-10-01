using Microsoft.Extensions.Caching.Distributed;
using MonstersIncDomain;
using MonstersIncLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonstersInc
{
    public class IntimidatorIntimidationsSummery
    {
        #region members
        private readonly IintimidatorWorkdayRepository _intimidatorWorkdayRepository;
        private readonly IDoorRepository _doorRepository;
        private readonly IintimidatorRepository _intimidatorRepository;
        private readonly IDistributedCache _distributedCache;

        private readonly int intimidatorId;
        #endregion

        public IntimidatorIntimidationsSummery(IDoorRepository _doorRepository
                                          , IintimidatorWorkdayRepository _intimidatorWorkdayRepository
                                          , IintimidatorRepository _intimidatorRepository
                                          , IDistributedCache _distributedCache
                                          , int intimidatorId)
        {
            this._doorRepository = _doorRepository ?? throw new ArgumentNullException(nameof(_doorRepository)); ;
            this._intimidatorWorkdayRepository = _intimidatorWorkdayRepository ?? throw new ArgumentNullException(nameof(_intimidatorWorkdayRepository)); ;
            this._intimidatorRepository = _intimidatorRepository ?? throw new ArgumentNullException(nameof(_intimidatorRepository));
            this._distributedCache = _distributedCache ?? throw new ArgumentNullException(nameof(_distributedCache));
            this.intimidatorId = intimidatorId;
        }
        /// <summary>
        /// will return intimidatorId summery and save result on _distributedCache
        /// </summary>
        /// <returns>List<IntimidatorWorkdayDto></returns>
        public async Task<List<IntimidatorWorkdayDto>> ReadAndCacheAsync()
        {
            var results = await _intimidatorWorkdayRepository.GetDailyWork(intimidatorId);
            var doors = await _doorRepository.SelectAsync(results.Select(d => d.DoorId).ToList());
            var intimidator = await _intimidatorRepository.SelectAsync(intimidatorId, null);

            ILookup<DateTime, IntimidatorIntimidation> days =
                              results
                             .ToLookup(p => p.Day);


            List<IntimidatorWorkdayDto> workdays = new List<IntimidatorWorkdayDto>();

            foreach (var day in days)
            {
                List<int> doorIds = days[day.Key].Select(d => d.DoorId).ToList();
                List<int> doorIdsDepleted = days[day.Key].Where(d => d.Depleted).Select(d => d.DoorId).ToList();

                var doorsForCurrentDay = doors.Where(door => doorIds.Contains(door.Id)).ToList();
                var doorsDepletedForCurrentDay = doors.Where(door => doorIdsDepleted.Contains(door.Id)).ToList();

                var workDayEnergy = doorsDepletedForCurrentDay.Sum(e => e.Energy);

                TimeSpan span = Time.GetSystemNow() - intimidator.StartToScareData;

                IntimidatorWorkdayDto workday = new IntimidatorWorkdayDto()
                {
                    Doors = doorsForCurrentDay,
                    Day = day.Key,
                    DailyEnergy = workDayEnergy,
                    GoalAccomplished = new CalculateEnergy(span.Days, workDayEnergy).IsWorkdayEnergyValid()
                };

                workdays.Add(workday);
            }

            await _distributedCache.SetStringAsync(GetWorkdaysCacheKey(), JsonSerializer.Serialize(workdays));

            return workdays;

        }
        public async Task<List<IntimidatorWorkdayDto>> ReadFromCacheAsync()
        {
            try
            {
                string cacheKey = GetWorkdaysCacheKey();
                string workdaysJson = await _distributedCache.GetStringAsync(cacheKey);

                if (workdaysJson != null)
                    return JsonSerializer.Deserialize<List<IntimidatorWorkdayDto>>(workdaysJson);

            }
            catch (Exception)
            {

            }

            return null;
        }
        #region PrivateMethods
        private string GetWorkdaysCacheKey()
        {
            return "workdays_cache_" + intimidatorId;
        }
        #endregion
    }
}
