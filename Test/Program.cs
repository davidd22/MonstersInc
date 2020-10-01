using MonstersIncLogic;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isvalid = new CalculateEnergy(DateTime.UtcNow.AddYears(-4), 20).IsWorkdayEnergyValid();
             isvalid = new CalculateEnergy(DateTime.UtcNow.AddYears(-3), 20).IsWorkdayEnergyValid();
             isvalid = new CalculateEnergy(DateTime.UtcNow.AddYears(-0), 20).IsWorkdayEnergyValid();
             isvalid = new CalculateEnergy(DateTime.UtcNow.AddYears(-10), 20).IsWorkdayEnergyValid();
        }
    }
}
