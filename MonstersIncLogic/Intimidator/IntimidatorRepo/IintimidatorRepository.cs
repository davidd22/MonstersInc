using MonstersIncDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonstersIncLogic
{
    public interface IintimidatorRepository
    {
        Task<Intimidator> InsertAsync(Intimidator intimidator, string cryptoPwd);
        Task<Intimidator> SelectAsync(int id, string cryptoPwd);
        Task<bool> IsPhoneExistsAsync(string phoneNumber);
        Task<Intimidator> SelectByClientIdAsync(string clientId, string cryptoPwd);
        Task<List<Intimidator>> SelectAllAsync();
    }
}
