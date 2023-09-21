using System;
using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class WeaponInstance : ObjectInstance
    {
        //TODO: BonusStats but without default bonus stats
        private List<StatData> _currentBonus;

        public List<StatData> CurrentStats => _currentStats;

        private WeaponStatsData WeaponStatsData => (WeaponStatsData)_statsData;

        public WeaponInstance(WeaponStatsData statsData) : base(statsData)
        {
        }

        public void AddValueToBonus(Stats stat, float addValue)
        {
            foreach (var bonus in _currentBonus)
            {
                if (bonus.Stat == stat)
                {
                    bonus.Value += addValue;
                    UpdateStatWithBonus(stat, bonus.Value);
                }
            }
        }

        public StatData GetStatBonus(Stats stats)
        {
            foreach (var bonus in _currentBonus)
            {
                if (bonus.Stat == stats) return bonus;
            }

            return new StatData();
        }

        protected override void SetupStat()
        {
            _currentBonus = new List<StatData>();
            base.SetupStat();
        }

        protected override void SetStat(StatData statData)
        {
            SetStatByName(statData.Stat, statData.Value);
        }

        public override void SetStatByName(Stats stat, float value)
        {
            if (CheckIgnore(stat)) return;
            if (!CheckExceptions(new StatData(stat, value))) return;

            base.SetStatByName(stat, value);
        }

        private void UpdateStatWithBonus(Stats stat, float bonusValue)
        {
            var defaultValue = GetDefaultStatByName(stat).Value;
            UpdateStatWithBonus(stat, bonusValue, defaultValue);
        }
        
        private void UpdateStatWithBonus(Stats stat, float bonusValue, float defaultValue)
        {
            float newValue = 0;
            switch (stat)
            {
                case Stats.Damage:
                case Stats.ProjectilesSpeed:
                case Stats.Area:
                case Stats.Duration:
                case Stats.Cooldown:
                    newValue = CalculateNewValue(defaultValue, bonusValue);
                    SetStatByName(stat, newValue);
                    break;
                case Stats.Amount:
                    newValue = bonusValue + defaultValue;
                    base.SetStatByName(stat, newValue);
                    break;
            }
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

        private bool CheckIgnore(Stats stat)
        {
            return WeaponStatsData.IgnoreStat.Contains(stat);
        }
    }
}