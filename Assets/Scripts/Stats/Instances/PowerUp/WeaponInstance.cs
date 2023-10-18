using System;
using System.Collections.Generic;
using Stats.ScriptableObjects;
using Stats.StatsCalculators;

namespace Stats.Instances.PowerUp
{
    public class WeaponInstance : PowerUpInstance
    {
        public WeaponStatCalculator WeaponStatCalculator => (WeaponStatCalculator)StatsCalculator;
        
        private WeaponStatsData WeaponStatsData => (WeaponStatsData)_statsData;
        
        
        public WeaponInstance(WeaponStatsData statsData) : base(statsData)
        {
        }

        private protected override void Setup()
        {
            var weaponStatCalculator = new WeaponStatCalculator(this, WeaponStatsData);
            weaponStatCalculator.CalculateCurrentStats();
            SetStatCalculator(weaponStatCalculator);
        }

        public override void AddBonusesFromItems(Dictionary<Stats, float> allClearItemBonus,
            Dictionary<Stats, float> allPercentItemBonus)
        {
            WeaponStatCalculator.RewriteOrAddOutsideBonus(allClearItemBonus, allPercentItemBonus);
            UpdateCurrentStats();
        }

        private protected bool IsNecessaryStat(Stats statData)
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
        }

        private bool CheckIgnore(Stats stat)
        {
            return WeaponStatsData.IgnoreStat.Contains(stat);
        }
    }
}