using System.Collections.Generic;
using Stats.Instances;
using Stats.Instances.PowerUp;

namespace Stats.StatsCalculators
{
    public class PlayerStatCalculator : PowerUpStatCalculator
    {
        private Dictionary<Stats, float> _clearBuffs;
        private Dictionary<Stats, float> _percentBuffs;

        public PlayerStatCalculator(ObjectInstance objectInstance) : base(objectInstance)
        {
            _clearBuffs = new Dictionary<Stats, float>();
            _percentBuffs = new Dictionary<Stats, float>();
        }

        public virtual void RewriteOrAddBuffs(Dictionary<Stats, float> allClearBuff,
            Dictionary<Stats, float> allPercentBuff)
        {
            _clearBuffs = allClearBuff;
            _percentBuffs = allPercentBuff;
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

        private protected override float GetClearBonusValue(Stats stat)
        {
            var clearBuff = GetValueFormDictionary(stat, _clearBuffs);
            return base.GetClearBonusValue(stat) + clearBuff;
        }

        private protected override float GetPercentBonusValue(Stats stat)
        {
            var percentBuff = GetValueFormDictionary(stat, _percentBuffs);
            return base.GetClearBonusValue(stat) + percentBuff;
        }
    }
}