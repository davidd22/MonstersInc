using MonstersIncData;
using MonstersIncDomain;
using System.Threading.Tasks;
using System;
using MonstersIncLogic.Helper;

namespace MonstersIncLogic
{
    public class IntimidationRepository : IintimidationRepository
    {
        private readonly MonstersIncContext _context;

        public IntimidationRepository(MonstersIncContext context)
        {
            _context = context;
        }

        public async Task<bool> End(int doorId, int IntimidatorId)
        {
            IntimidatorIntimidation intimidation = await new IntimidationData(_context).SelectAsync(Time.GetSystemNow().Date, doorId, IntimidatorId);

            if (intimidation == null)
                return false;

            intimidation.Depleted = true;

            return await new IntimidationData(_context).End(intimidation);
        }

        public async Task<bool> Start(int doorId, int IntimidatorId)
        {
            return await new IntimidationData(_context).Start(new IntimidatorIntimidation()
            {
                DoorId = doorId,
                IntimidatorId = IntimidatorId,
                Day = Time.GetSystemNow()
            });
        }
    }
}
