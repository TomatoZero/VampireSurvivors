using System;
using System.Collections.Generic;
using Stats.ScriptableObjects;
using Stats.StatsCalculators;

namespace Stats.Instances
{
    [Serializable]
    public abstract class ObjectInstance
    {
        private protected ObjectStatsData _statsData;
        private  int _currentLvl = 1;
        private StatsCalculator _statsCalculator;

        public ObjectStatsData StatsData => _statsData;
        public int CurrentLvl => _currentLvl;
        public int MaxLevel => _statsData.MaxLvl;
        public StatsCalculator StatsCalculator => _statsCalculator;
        

        public ObjectInstance(ObjectStatsData statsData)
        {
            _statsData = statsData;
            Setup();
        }

        private protected abstract void Setup();

        public void UpdateClearAndPercentStats()
        {
            StatsCalculator.CalculatePercentBonuses();
            StatsCalculator.CalculateClearBonuses();
        }

        public void UpdateCurrentStats()
        {
            StatsCalculator.CalculateCurrentStats();
        }
        
        public virtual StatData GetDefaultStatByName(Stats stat)
        {
            foreach (var statData in _statsData.DefaultStatsData)
            {
                if (statData.Stat.Equals(stat)) return statData;
            }

            return new StatData();
        }

        public virtual StatData GetStatByName(Stats stat)
        {
            foreach (var statData in _statsCalculator.CurrentStats)
            {
                if (statData.Stat.Equals(stat)) return statData;
            }

            return new StatData();
        }

        private protected void IncreaseLevel()
        {
            _currentLvl++;
        }

        private protected void SetStatCalculator(StatsCalculator calculator)
        {
            _statsCalculator = calculator;
        }
        
        public abstract void LevelUp();
    }
}