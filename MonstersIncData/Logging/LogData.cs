using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonstersIncData
{
    public class LogData : MonsterIncDataAbs
    {
        public LogData(MonstersIncContext context) : base(context)
        {

        }
        public async Task<bool> LogAsync(Log LogEntry)
        {
            try
            {
                _context.Logs.Add(LogEntry);
                int affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
