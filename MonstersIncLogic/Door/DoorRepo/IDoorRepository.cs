using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonstersIncLogic
{
    public interface IDoorRepository
    {
        Task<List<Door>> SelectAvailableAsync();
        Task<List<Door>> SelectAsync(List<int> enumerable);
    }
}
