using Microsoft.EntityFrameworkCore;
using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersIncData
{
    public class IntimidatorData : MonsterIncDataAbs
    {
        public IntimidatorData(MonstersIncContext context) : base(context)
        {

        }

        public async Task<Intimidator> InsertAsync(Intimidator intimidator)
        {
            try
            {
                _context.Intimidators.Add(intimidator);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                    return intimidator;
            }
            catch (Exception)
            {


            }

            return null;
        }

        public async Task<Intimidator> SelectAsync(int id)
        {
            try
            {
                return await _context.Intimidators.Where(c => c.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<bool> IsPhoneExistsAsync(string phoneNumber)
        {
            try
            {
                return await _context.Intimidators.Where(c => c.PhoneNumber == phoneNumber).CountAsync() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public async Task<List<Intimidator>> SelectAllAsync()
        {
            return await _context.Intimidators.ToListAsync();
        }

        public async Task<Intimidator> SelectByClientIdAsync(string clientId)
        {
            try
            {
                return await _context.Intimidators.Where(c => c.ClientId == clientId).SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
