using System;
using System.Collections.Generic;
using System.Text;

namespace MonstersIncLogic
{
    public class CalculateEnergy
    {
        private const int FIRST_YEAR_CAP = 100;
        private const int EXTRA_YEAR_ADDED_CAP = 20;


        int startToScareDays;
        private readonly float workdayEnergy;

        public CalculateEnergy(int startToScareDays, float workdayEnergy)
        {
            this.startToScareDays = startToScareDays;
            this.workdayEnergy = workdayEnergy;
        }

        public bool IsWorkdayEnergyValid()
        {
            int requiredEnergyLevel = GetRequiredEnergyLevel();
            return workdayEnergy >= requiredEnergyLevel;
        }
        private int GetRequiredEnergyLevel()
        {
            int years = startToScareDays / 365;

            if (years <= 1)
                return FIRST_YEAR_CAP;

            else
            {
                return FIRST_YEAR_CAP + (EXTRA_YEAR_ADDED_CAP * (years - 1));
            }
        }
    }
}
