using MonstersIncData;
using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonstersIncLogic
{
    public class DoorRepository : IDoorRepository
    {
        private readonly MonstersIncContext _context;

        public DoorRepository(MonstersIncContext context)
        {
            _context = context;
        }

        public async Task<List<Door>> SelectAsync(List<int> ids)
        {
            return await new DoorData(_context).SelectAsync(ids);
        }

        public async Task<List<Door>> SelectAvailableAsync()
        {
            return await new DoorData(_context).SelectAvailableAsync(Time.GetSystemNow().Date);
        }
    }
}
