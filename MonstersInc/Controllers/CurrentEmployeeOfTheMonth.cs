using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using MonstersInc.Auth;
using MonstersInc.Models;
using MonstersIncDomain;
using MonstersIncLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc.Controllers
{
    [Route("api/CurrentEmployeeOfTheMonth")]
    [ApiController]
    public class CurrentEmployeeOfTheMonth : ControllerBase
    {
        private readonly IintimidatorWorkdayRepository _intimidatorWorkdayRepository;
        private readonly IDoorRepository _doorRepository;
        private readonly IintimidatorRepository _intimidatorRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public CurrentEmployeeOfTheMonth(IDoorRepository _doorRepository
                                          , IintimidatorWorkdayRepository _intimidatorWorkdayRepository
                                          , IintimidatorRepository _intimidatorRepository
                                          , IDistributedCache _distributedCache
                                          , IMapper _mapper
            )
        {
            this._doorRepository = _doorRepository ?? throw new ArgumentNullException(nameof(_doorRepository)); ;
            this._intimidatorWorkdayRepository = _intimidatorWorkdayRepository ?? throw new ArgumentNullException(nameof(_intimidatorWorkdayRepository)); ;
            this._intimidatorRepository = _intimidatorRepository ?? throw new ArgumentNullException(nameof(_intimidatorRepository));
            this._distributedCache = _distributedCache ?? throw new ArgumentNullException(nameof(_distributedCache));
            this._mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int goalAccomplishedMax = 0;
            HashSet<CurrentEmployeeOfTheMonthDto> employeesOfTheMonthSet = new HashSet<CurrentEmployeeOfTheMonthDto>();
            List<Intimidator> intimidators = await _intimidatorRepository.SelectAllAsync();

            DateTime startOfMonth = Time.GetStartOfMonth();
            DateTime endOfMonth = Time.GetEndOfMonth();

            for (int i = 0; i < intimidators.Count; i++)
            {
                var intimidatorSummery = new IntimidatorIntimidationsSummery(_doorRepository
                                                                    , _intimidatorWorkdayRepository
                                                                    , _intimidatorRepository
                                                                    , _distributedCache
                                                                    , intimidators[i].Id);


                List<IntimidatorWorkdayDto> intimidatorResult = await intimidatorSummery.ReadFromCacheAsync();

                if (intimidatorResult == null)
                    intimidatorResult = await intimidatorSummery.ReadAndCacheAsync();

                if (intimidatorResult.Count > 0)
                {
                    intimidatorResult = intimidatorResult.Where(c => c.Day >= startOfMonth && c.Day <= endOfMonth).ToList();
                    List<IntimidatorWorkdayDto> withGoalsAccomplished = intimidatorResult.Where(g => g.GoalAccomplished).ToList();


                    int goalsAccomplished = withGoalsAccomplished.Count();

                    if (goalsAccomplished > goalAccomplishedMax)
                    {
                        goalAccomplishedMax = goalsAccomplished;

                        CurrentEmployeeOfTheMonthDto dto = _mapper.Map<CurrentEmployeeOfTheMonthDto>(intimidators[i]);
                        dto.GoalsAccomplished = goalsAccomplished;
                        dto.EnergyCollected = withGoalsAccomplished.Sum(e => e.DailyEnergy);

                        employeesOfTheMonthSet.Add(dto);
                    }

                    // float energyCollected = intimidatorResult.Where(g => g.GoalAccomplished).Sum(a => a.DailyEnergy);
                }
            }

            return Ok(employeesOfTheMonthSet);
        }
    }
}
