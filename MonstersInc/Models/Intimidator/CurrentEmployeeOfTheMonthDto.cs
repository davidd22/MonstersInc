using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc
{
    public class CurrentEmployeeOfTheMonthDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TentaclesNumber { get; set; }
        public DateTime StartToScareData { get; set; }

        public int GoalsAccomplished { get; set; }
        public float EnergyCollected { get; set; }
    }
}
