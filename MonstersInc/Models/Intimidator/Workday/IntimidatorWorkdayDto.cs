using MonstersIncDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc
{
    public class IntimidatorWorkdayDto
    {
        public DateTime Day { get; set; }
        public bool GoalAccomplished { get; set; }
        public float DailyEnergy { get; set; }
        public List<Door> Doors { get; set; }
    }
}
