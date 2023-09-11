using System.Collections.Generic;
using Stats.ScriptableObjects;
        
namespace Stats.Instances
{
    public class WeaponInstance : ObjectInstance
    {
        public List<StatData> CurrentStats => _currentStats;        
        
        private WeaponStatsData WeaponStatsData => (WeaponStatsData)_statsData;

        public WeaponInstance(WeaponStatsData statsData) : base(statsData)
        {
        }

        public override void SetStatByName(Stats stat, int value)
        {
            if(CheckIgnore(stat)) return;
            
            base.SetStatByName(stat, value);
        }

        private bool CheckIgnore(Stats stat)
        {
            return WeaponStatsData.IgnoreStat.Contains(stat);
        }
    }
}