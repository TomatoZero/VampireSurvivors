using Stats.Instances;
using Stats.Instances.PowerUp;

namespace Stats.StatsCalculators
{
    public class PlayerStatCalculator : UnitStatCalculator
    {
        public PlayerStatCalculator(ObjectInstance objectInstance) : base(objectInstance)
        {
        }

        private protected override void SeparateDefaultStats(ObjectInstance instance)
        {
            base.SeparateDefaultStats(instance);

            var playerBonusStat = ((PlayerInstance)instance).PlayerStatsData.BonusStats;

            foreach (var statData in playerBonusStat)
            {
                if (statData.IsPercent)
                    DefaultsStatPercent.Add(statData.Stat, statData.Value);
                else
                    DefaultsStatClear.Add(statData.Stat, statData.Value);
            }
        }
    }
}