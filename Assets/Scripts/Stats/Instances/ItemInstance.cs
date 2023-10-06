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
            
            // Debug.Log($"_statsData.LevelUpBonuses[{_currentLvl - 1}] = {_statsData.LevelUpBonuses[_currentLvl - 1].BonusStat[0].Value}");
            
            var lvlUpStatsData = _statsData.LevelUpBonuses[_currentLvl - 1];

            foreach (var statData in lvlUpStatsData.BonusStat)
            {
                var currentValue = GetStatByName(statData.Stat).Value;
                SetStatByName(statData.Stat, currentValue + statData.Value);
            }
            
            _currentLvl++;
        }
    }
}