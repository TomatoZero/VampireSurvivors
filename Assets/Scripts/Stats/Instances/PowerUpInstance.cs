using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public abstract class PowerUpInstance : ObjectInstance
    {
        private List<StatData> _currentBonus;

        public List<StatData> CurrentStats
        {
            get => _currentStats;
            private protected set => _currentStats = value;
        }
        
        public List<StatData> CurrentBonus
        {
            get => _currentBonus;
            private protected set => _currentBonus = value;
        }

        public PowerUpInstance(ObjectStatsData statsData) : base(statsData)
        {
        }

        public override void LevelUp()
        {
            if (_statsData.MaxLvl <= _currentLvl) return;

            var lvlUpStatsData = _statsData.LevelUpBonuses[_currentLvl - 2];

            foreach (var statData in lvlUpStatsData.BonusStat)
            {
                AddValueToBonus(statData.Stat, statData.Value);
            }

            _currentLvl++;
        }

        public virtual void AddValueToBonus(Stats stat, float addValue)
        {
            foreach (var bonus in CurrentBonus)
            {
                if (bonus.Stat == stat)
                {
                    bonus.Value += addValue;
                    UpdateStatWithBonus(stat, bonus.Value);
                    return;
                }
            }

            CurrentBonus.Add(new StatData(stat, addValue));
            UpdateStatWithBonus(stat, addValue);
        }

        public virtual StatData GetStatBonus(Stats stats)
        {
            foreach (var bonus in CurrentBonus)
            {
                if (bonus.Stat == stats) return bonus;
            }

            return new StatData();
        }

        private protected virtual void UpdateStatWithBonus(Stats stat, float bonusValue)
        {
            var defaultValue = GetDefaultStatByName(stat).Value;
            UpdateStatWithBonus(stat, bonusValue, defaultValue);
        }

        private protected abstract void UpdateStatWithBonus(Stats stat, float bonusValue, float defaultValue);
    }
}