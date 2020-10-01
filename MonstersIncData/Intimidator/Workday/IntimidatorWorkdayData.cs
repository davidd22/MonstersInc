using Microsoft.EntityFrameworkCore;
using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonstersIncData
{
    public class IntimidatorWorkdayData : MonsterIncDataAbs
    {
        public IntimidatorWorkdayData(MonstersIncContext context) : base(context)
        {

        }

        public async Task<List<IntimidatorIntimidation>> GetDailyWork(int intimidatorId)
        {
            try
            {
                return await _context
             .IntimidatorIntimidations
             .Where(b => b.IntimidatorId == intimidatorId).ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}
