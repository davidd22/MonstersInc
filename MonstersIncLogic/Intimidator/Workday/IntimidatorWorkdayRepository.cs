using MonstersIncData;
using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MonstersIncLogic
{
    public class IntimidatorWorkdayRepository : IintimidatorWorkdayRepository
    {
        private readonly MonstersIncContext _context;

        public IntimidatorWorkdayRepository(MonstersIncContext context)
        {
            _context = context;
        }

        public async Task<List<IntimidatorIntimidation>> GetDailyWork(int intimidatorId)
        {
            return await new IntimidatorWorkdayData(_context).GetDailyWork(intimidatorId);
        }
    }
}
