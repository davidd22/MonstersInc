using System;
using System.Collections.Generic;
using System.Text;

namespace MonstersIncData
{
    public abstract class MonsterIncDataAbs
    {
        protected readonly MonstersIncContext _context;

        protected MonsterIncDataAbs(MonstersIncContext context)
        {
            _context = context;
        }
    }
}
