using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class WeaponInstance : ObjectStatsInstance
    {
        public List<StatData> CurrentStats => _currentStats;
        
        private WeaponStatsData PlayerStatsData => (WeaponStatsData)_playerStatsData;

        public WeaponInstance(WeaponStatsData statsData) : base(statsData)
        {
        }
    }
}