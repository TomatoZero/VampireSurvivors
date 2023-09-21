using System;
using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class PlayerInstance : ObjectInstance
    {
        private int _lvl;
        private List<StatData> _currentBonus;

        public List<StatData> CurrentStats
        {
            get => _currentStats;
            private set => _currentStats = value;
        }

        public List<StatData> CurrentBonus => _currentBonus;

        public PlayerStatsData PlayerStatsData => (PlayerStatsData)_statsData;

        public PlayerInstance(PlayerStatsData playerStatsData) : base(playerStatsData)
        {
        }

        protected override void SetupStat()
        {
            base.SetupStat();
            _currentBonus = new List<StatData>();

            foreach (var bonus in PlayerStatsData.BonusStats)
            {
                _currentBonus.Add((StatData)bonus.Clone());
            }

            foreach (var currentBonus in _currentBonus)
            {
                UpdateStatWithBonus(currentBonus);
            }
        }

        //TODO: method for update bonus stat
        
        private void UpdateStatWithBonus(StatData bonusStatData)
        {
            var defaultValue = GetDefaultStatByName(bonusStatData.Stat).Value;
            UpdateStatWithBonus(bonusStatData.Stat, bonusStatData.Value, defaultValue);
        }

        private void UpdateStatWithBonus(Stats stat, float bonusValue, float defaultValue)
        {
            float newValue = 0;
            switch (stat)
            {
                case Stats.MaxHealth:
                case Stats.Armor:
                case Stats.MoveSpeed:
                case Stats.Recovery:
                    newValue = CalculateNewValue(defaultValue, bonusValue);
                    SetStatByName(stat, newValue);
                    break;
                case Stats.Luck:
                case Stats.Growth:
                case Stats.Greed:
                case Stats.Curse:
                    newValue = defaultValue + bonusValue;
                    SetStatByName(stat, newValue);
                    break;
            }
        }
    }
}