using MonstersIncDomain;
using System.Threading.Tasks;

namespace MonstersIncLogic
{
    public interface IintimidationRepository
    {
        Task<bool> Start(int doorId, int IntimidatorId);
        Task<bool> End(int doorId, int IntimidatorId);
    }
}
