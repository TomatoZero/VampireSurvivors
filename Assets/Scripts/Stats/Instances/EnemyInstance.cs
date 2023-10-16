using System;
using Stats.ScriptableObjects;
using Stats.StatsCalculators;

namespace Stats.Instances
{
    public class EnemyInstance : ObjectInstance
    {
        public EnemyInstance(EnemyStatsData statsData) : base(statsData)
        {
        }

        private protected override void Setup()
        {
            var statCalculator = new StatsCalculator(this);
            statCalculator.CalculateCurrentStats();
            SetStatCalculator(statCalculator);
        }

        public override void LevelUp()
        {
            throw new Exception("Enemy shouldn't have level up");
        }
    }
}