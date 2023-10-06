using System;
using System.Collections.Generic;
using Stats.ScriptableObjects;
using UnityEngine;

namespace Stats.Instances
{
    [Serializable]
    public class ItemInstance : ObjectInstance
    {
        public List<StatData> CurrentStats => _currentStats;

        public ItemInstance(ObjectStatsData statsData) : base(statsData)
        {
            
        }
        
        public override void LevelUp()
        {
            if(_statsData.MaxLvl <= _currentLvl) return;
            
            var lvlUpStatsData = _statsData.LevelUpBonuses[_currentLvl - 2];

            foreach (var statData in lvlUpStatsData.BonusStat)
            {
                var currentValue = GetStatByName(statData.Stat).Value;
                SetStatByName(statData.Stat, currentValue + statData.Value);
            }
            
            _currentLvl++;
        }
    }
}