using Microsoft.EntityFrameworkCore;
using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MonstersIncData
{
    public class DoorData : MonsterIncDataAbs
    {
        public DoorData(MonstersIncContext context) : base(context)
        {
        }

        public async Task<List<Door>> SelectAvailableAsync(DateTime today)
        {
            List<IntimidatorIntimidation> workingDoors = await _context
                            .IntimidatorIntimidations
                            .Where(b => b.Day == today).ToListAsync();

            List<Door> doors = await _context.Doors.ToListAsync();

            List<int> workingDoorsIds = workingDoors.Select(c => c.DoorId).ToList();

            return doors.Where(door => !workingDoorsIds.Contains(door.Id)).ToList();
        }

        public async Task<List<Door>> SelectAsync(List<int> ids)
        {
            try
            {
                return await _context
                      .Doors
                      .Where(p => ids.Contains(p.Id)).ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
