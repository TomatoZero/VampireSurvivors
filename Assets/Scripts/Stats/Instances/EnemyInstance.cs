using System;
using System.Collections.Generic;
using ScriptableObjects;
using Stats.Instances.PowerUp;
using Stats.StatsCalculators;

namespace Stats.Instances
{
    public class EnemyInstance : PowerUpInstance
    {
        public EnemyStatsData EnemyStatsData => (EnemyStatsData)_statsData;
        public PowerUpStatCalculator PowerUpStatCalculator => (UnitStatCalculator)StatsCalculator;
        
        public EnemyInstance(EnemyStatsData statsData) : base(statsData)
        {
        }

        private protected override void Setup()
        {
            var statCalculator = new UnitStatCalculator(this);
            statCalculator.CalculateCurrentStats();
            SetStatCalculator(statCalculator);
        }

        public override void AddBonusesFromItems(Dictionary<Stats, float> allClearItemBonus, Dictionary<Stats, float> allPercentItemBonus)
        {
            PowerUpStatCalculator.RewriteOrAddOutsideBonus(allClearItemBonus, allPercentItemBonus);
            UpdateCurrentStats();
        }
    }
}