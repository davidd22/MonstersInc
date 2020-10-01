using MonstersIncData;
using MonstersIncDomain;
using System.Threading.Tasks;
using System;
using MonstersIncLogic.Helper;
using System.Collections.Generic;

namespace MonstersIncLogic
{
    public class IntimidatorRepository : IintimidatorRepository
    {
        private readonly MonstersIncContext _context;

        public IntimidatorRepository(MonstersIncContext context)
        {
            _context = context;
        }
        public async Task<Intimidator> InsertAsync(Intimidator intimidator, string cryptoPwd)
        {
            intimidator.ClientId = Guid.NewGuid().ToString("N");
            intimidator.ClientSecret = new PasswordCrypto(intimidator.ClientSecret, cryptoPwd).Encrypt();
            intimidator.StartToScareData = Time.GetSystemNow();

            return await new IntimidatorData(_context).InsertAsync(intimidator);
        }

        public async Task<bool> IsPhoneExistsAsync(string phoneNumber)
        {
            return await new IntimidatorData(_context).IsPhoneExistsAsync(phoneNumber);
        }

        public async Task<List<Intimidator>> SelectAllAsync()
        {
            return await new IntimidatorData(_context).SelectAllAsync();
        }

        public async Task<Intimidator> SelectAsync(int id, string cryptoPwd)
        {
            var intimidator = await new IntimidatorData(_context).SelectAsync(id);

            if (intimidator != null && cryptoPwd != null)
                intimidator.ClientSecret = GetPlainPassword(intimidator.ClientSecret, cryptoPwd);

            return intimidator;
        }

        public async Task<Intimidator> SelectByClientIdAsync(string clientId, string cryptoPwd)
        {
            var intimidator = await new IntimidatorData(_context).SelectByClientIdAsync(clientId);

            if (intimidator != null && cryptoPwd != null)
                intimidator.ClientSecret = GetPlainPassword(intimidator.ClientSecret, cryptoPwd);

            return intimidator;
        }
        private string GetPlainPassword(string encPassword, string cryptoPwd)
        {
            return new PasswordCrypto(encPassword, cryptoPwd).Decrypt();
        }
    }
}
