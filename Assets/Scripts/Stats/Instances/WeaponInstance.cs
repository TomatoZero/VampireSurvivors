using System;
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

        public override void SetStat(StatData statData)
        {
            if(!CheckExceptions(statData)) return;
            
            base.SetStat(statData);
        }

        public override void SetStatByName(Stats stat, float value)
        {
            if (CheckIgnore(stat)) return;

            base.SetStatByName(stat, value);
        }

        private bool CheckIgnore(Stats stat)
        {
            return WeaponStatsData.IgnoreStat.Contains(stat);
        }

        private bool CheckExceptions(StatData statData)
        {
            if (statData.Stat == Stats.Luck)
            {
                SetChanceStat(statData);
                return false;
            }
            else if (statData.Stat == Stats.Chance)
            {
                throw new ArgumentException();
            }
            
            return true;
        }
        
        private void SetChanceStat(StatData statData)
        {
            var defaultChance = GetDefaultStatByName(Stats.Chance);
            var newChance = statData.Value * defaultChance.Value;
            var newChanceStat = new StatData(Stats.Chance, newChance);

            base.SetStat(newChanceStat);
        }
    }
}