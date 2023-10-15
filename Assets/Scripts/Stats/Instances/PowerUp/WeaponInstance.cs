using System;
using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class WeaponInstance : PowerUpInstance
    {
        private WeaponStatsData WeaponStatsData => (WeaponStatsData)_statsData;

        public WeaponInstance(WeaponStatsData statsData) : base(statsData)
        {
        }

        protected override void SetupStat()
        {
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

        private protected override void UpdateStatWithBonus(Stats stat, float bonusValue, float defaultValue)
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

        private protected override bool IsNecessaryStat(Stats statData)
        {
            switch (statData)
            {
                case Stats.MaxHealth:
                case Stats.MoveSpeed:
                case Stats.Armor:
                case Stats.Magnet:
                case Stats.Revival:
                case Stats.Recovery:
                case Stats.Reroll:
                case Stats.Skip:
                    return false;
            }

            return true;
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
                throw new ArgumentException("Stat Chance is unacceptable");
            }

            return true;
        }

        private void SetChanceStat(StatData statData)
        {
            var defaultChance = GetDefaultStatByName(Stats.Chance);
            var newChance = statData.Value * defaultChance.Value;

            base.SetStatByName(Stats.Chance, newChance);
        }

        private bool CheckIgnore(Stats stat)
        {
            return WeaponStatsData.IgnoreStat.Contains(stat);
        }
    }
}