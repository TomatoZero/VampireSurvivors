using System;
using Stats.ScriptableObjects;
using Stats.StatsCalculators;

namespace Stats.Instances
{
    [Serializable]
    public class ItemInstance : ObjectInstance
    {
        public ItemInstance(ObjectStatsData statsData) : base(statsData)
        {
        }

        private protected override void Setup()
        {
            var statsCalculator = new StatsCalculator(this);
            statsCalculator.CalculateCurrentStats();
            SetStatCalculator(statsCalculator);
        }

        public override void LevelUp()
        {
            if (_statsData.MaxLvl <= CurrentLvl) return;

            var lvlUpStatsData = _statsData.LevelUpBonuses[CurrentLvl - 1];

            foreach (var statData in lvlUpStatsData.BonusStat)
            {
                StatsCalculator.AddLevelUpBonus(statData);
            }

            UpdateClearAndPercentStats();
            IncreaseLevel();
        }
    }
}