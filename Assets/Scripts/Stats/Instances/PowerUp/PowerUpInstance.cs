using System.Collections.Generic;
using Stats.ScriptableObjects;
using Stats.StatsCalculators;

namespace Stats.Instances.PowerUp
{
    public abstract class PowerUpInstance : ObjectInstance
    {
        public List<StatData> CurrentStats
        {
            get => _currentStats;
            private protected set => _currentStats = value;
        }

        public PowerUpStatCalculator PowerUpStatCalculator => (PowerUpStatCalculator)StatsCalculator;

        protected PowerUpInstance(ObjectStatsData statsData) : base(statsData)
        { }

        private protected override void Setup()
        {
            var powerUpStatCalculator = new PowerUpStatCalculator(this);
            _currentStats = powerUpStatCalculator.CalculateCurrentStats();
            SetStatCalculator(powerUpStatCalculator);
        }

        public override void LevelUp()
        {
            if (_statsData.MaxLvl <= CurrentLvl) return;

            var lvlUpStatsData = _statsData.LevelUpBonuses[CurrentLvl - 1];
            foreach (var statData in lvlUpStatsData.BonusStat)
            {
                AddValueToLevelUpBonus(statData);
            }

            IncreaseLevel();
        }

        public virtual void AddValueToBonus(StatData statData)
        {
            PowerUpStatCalculator.RewriteOrAddOutsideBonus(statData);
        }

        private protected virtual void AddValueToLevelUpBonus(StatData statData)
        {
            PowerUpStatCalculator.AddLevelUpBonus(statData);
        }

        private float GetBonusValue(List<StatData> list, Stats stat)
        {
            foreach (var outsideBonus in list)
            {
                if (outsideBonus.Stat == stat) return outsideBonus.Value;
            }

            return 0f;
        }
    }
}