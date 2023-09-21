using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public abstract class ObjectInstance
    {
        //TODO: default stat data, _statsData > _bonusStatData
        private protected ObjectStatsData _statsData;
        private protected List<StatData> _currentStats;

        public ObjectInstance(ObjectStatsData statsData)
        {
            _statsData = statsData;
            SetupStat();
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
            foreach (var statData in _currentStats)
            {
                if (statData.Stat.Equals(stat)) return statData;
            }

            return new StatData();
        }

        public virtual void SetStat(StatData statData)
        {
            SetStatByName(statData.Stat, statData.Value);
        }
        
        public virtual void SetStatByName(Stats stat, float value)
        {
            foreach (var statData in _currentStats)
            {
                if (statData.Stat.Equals(stat)) statData.Value = value;
            }
        }

        protected virtual void SetupStat()
        {
            _currentStats = new List<StatData>();

            foreach (var stat in _statsData.DefaultStatsData)
                _currentStats.Add((StatData)stat.Clone());
        }
    }
}