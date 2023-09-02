using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class PlayerStats
    {
        private PlayerStatsData _playerStatsData;
        private List<StatData> _currentStats;

        public List<StatData> CurrentStats => _currentStats;

        public PlayerStats(PlayerStatsData playerStatsData)
        {
            _playerStatsData = playerStatsData;
            CopyStats();
        }

        public StatData GetStatByName(Stats stat)
        {
            foreach (var statData in _currentStats)
            {
                if (statData.Stat.Equals(stat)) return statData;
            }

            return new StatData();
        }

        public void SetStatByName(Stats stat, int value)
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