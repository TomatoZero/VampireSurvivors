using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public abstract class ObjectStatsInstance
    {
        private protected ObjectStatsData _playerStatsData;
        private protected List<StatData> _currentStats;

        public ObjectStatsInstance(ObjectStatsData statsData)
        {
            _playerStatsData = statsData;
            CopyStats();
        }
        
        public virtual StatData GetStatByName(Stats stat)
        {
            foreach (var statData in _currentStats)
            {
                if (statData.Stat.Equals(stat)) return statData;
            }

            return new StatData();
        }

        public virtual void SetStatByName(Stats stat, int value)
        {
            foreach (var statData in _currentStats)
            {
                if (statData.Stat.Equals(stat)) statData.Value = value;
            }
        }
        
        private void CopyStats()
        {
            _currentStats = new List<StatData>();

            foreach (var stat in _playerStatsData.Stats)
                _currentStats.Add((StatData)stat.Clone());
        }
    }
}