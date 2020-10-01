using Microsoft.Extensions.Logging;
using MonstersIncData;
using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonstersIncLogic
{
    public class LogRepository : ILogRepository
    {
        private readonly MonstersIncContext _context;

        public LogRepository(MonstersIncContext context)
        {
            _context = context;
        }


        public async Task<bool> LogAsync(Log LogEntry)
        {
            return await new LogData(_context).LogAsync(LogEntry);
        }
    }
}
