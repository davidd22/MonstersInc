using MonstersIncDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonstersIncLogic
{
    public interface IintimidatorWorkdayRepository
    {
        public Task<List<IntimidatorIntimidation>> GetDailyWork(int intimidatorId);
    }
}
