using Microsoft.EntityFrameworkCore;
using MonstersIncDomain;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace MonstersIncData
{
    public class IntimidationData : MonsterIncDataAbs
    {
        public IntimidationData(MonstersIncContext context) : base(context)
        {
        }


        public async Task<bool> End(IntimidatorIntimidation intimidatorIntimidation)
        {
            try
            {
                _context.IntimidatorIntimidations.Update(intimidatorIntimidation);
                int affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IntimidatorIntimidation> SelectAsync(DateTime date, int doorId, int intimidatorId)
        {
            try
            {
                return await _context.IntimidatorIntimidations.Where(b => b.Day == date && b.DoorId == doorId && b.IntimidatorId == intimidatorId).SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> Start(IntimidatorIntimidation intimidatorIntimidation)
        {
            try
            {
                _context.IntimidatorIntimidations.Add(intimidatorIntimidation);
                int affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
