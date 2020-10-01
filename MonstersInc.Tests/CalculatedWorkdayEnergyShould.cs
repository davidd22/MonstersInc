using MonstersIncLogic;
using System;
using Xunit;

namespace MonstersInc.Tests
{
    public class CalculatedWorkdayEnergyShould
    {

        [Theory]
        [InlineData(363, 100)]
        [InlineData(363, 200)]
        public void BeValidForNewIintimidator(int startToScareDays, int dailyEnergyLevel)
        {
            var valid = new CalculateEnergy(startToScareDays, dailyEnergyLevel).IsWorkdayEnergyValid();

            Assert.True(valid);
        }
        [Theory]
        [InlineData(363, 50)]
        [InlineData(36, 0)]
        public void BeNonValidForNewIintimidator(int startToScareDays, int dailyEnergyLevel)
        {
            var valid = new CalculateEnergy(startToScareDays, dailyEnergyLevel).IsWorkdayEnergyValid();

            Assert.True(!valid);
        }
        [Theory]
        [InlineData(1095, 140)]
        [InlineData(1095, 170)]
        [InlineData(1095, 170234)]
        public void BeValidForThreeYearsIintimidator(int startToScareDays, int dailyEnergyLevel)
        {
            var valid = new CalculateEnergy(startToScareDays, dailyEnergyLevel).IsWorkdayEnergyValid();

            Assert.True(valid);
        }
        [Theory]
        [InlineData(1095, 100)]
        [InlineData(1095, 139)]
        [InlineData(1095, 120)]
        public void BeNonValidForThreeYearsIintimidator(int startToScareDays, int dailyEnergyLevel)
        {
            var valid = new CalculateEnergy(startToScareDays, dailyEnergyLevel).IsWorkdayEnergyValid();

            Assert.True(!valid);
        }
    }
}
