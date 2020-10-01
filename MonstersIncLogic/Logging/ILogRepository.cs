using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonstersIncLogic
{
    public interface ILogRepository
    {
        public Task<bool> LogAsync(Log LogEntry);
    }
}
