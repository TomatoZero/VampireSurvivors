using System;
using System.Collections.Generic;
using Stats.ScriptableObjects;
using Stats.StatsCalculators;

namespace Stats.Instances
{
    [Serializable]
    public class ItemInstance : ObjectInstance
    {
        public List<StatData> CurrentStats => _currentStats;

        public ItemInstance(ObjectStatsData statsData) : base(statsData)
        {
        }

        private protected override void Setup()
        {
            var statsCalculator = new StatsCalculator(this);
            _currentStats = statsCalculator.CalculateCurrentStats();
            SetStatCalculator(statsCalculator);
        }

        public override void LevelUp()
        {
            if (_statsData.MaxLvl <= CurrentLvl) return;

            // Debug.Log($"_statsData.LevelUpBonuses[{_currentLvl - 1}] = {_statsData.LevelUpBonuses[_currentLvl - 1].BonusStat[0].Value}");

            var lvlUpStatsData = _statsData.LevelUpBonuses[CurrentLvl - 1];

            foreach (var statData in lvlUpStatsData.BonusStat)
            {
                StatsCalculator.AddLevelUpBonus(statData);
            }

            IncreaseLevel();
        }
    }
}