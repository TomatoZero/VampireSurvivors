using System.Collections.Generic;
using Stats.Instances;

namespace Stats.StatsCalculators
{
    public class PowerUpStatCalculator : StatsCalculator
    {
        private Dictionary<Stats, float> _clearBonusFromOutside;
        private Dictionary<Stats, float> _percentBonusFromOutside;

        public PowerUpStatCalculator(ObjectInstance objectInstance) : base(objectInstance)
        {
            _clearBonusFromOutside = new Dictionary<Stats, float>();
            _percentBonusFromOutside = new Dictionary<Stats, float>();
        }

        public void RewriteOrAddOutsideBonus(StatData statData)
        {
            if (statData.IsPercent)
            {
                _percentBonusFromOutside[statData.Stat] = statData.Value;
            }
            else
            {
                _clearBonusFromOutside[statData.Stat] = statData.Value;
            }
        }

        public override List<StatData> CalculateCurrentStats()
        {
            return base.CalculateCurrentStats();
        }

        private protected override float GetClearBonusValue(Stats stat)
        {
            var clearBonusFromOutside = GetValueFormDictionary(stat, _clearBonusFromOutside);
            return base.GetClearBonusValue(stat) + clearBonusFromOutside;
        }

        private protected override float GetAllPercentBonusValue(Stats stat)
        {
            var percentBonusFromOutside = GetValueFormDictionary(stat, _percentBonusFromOutside);
            return base.GetAllPercentBonusValue(stat) + percentBonusFromOutside;
        }

        private protected override HashSet<Stats> GetClearStats()
        {
            var stats = base.GetClearStats();

            foreach (var data in _clearBonusFromOutside) stats.Add(data.Key);

            return stats;
        }

        private protected override HashSet<Stats> GetPercentBonusStats()
        {
            var stats = base.GetPercentBonusStats();
            foreach (var data in _percentBonusFromOutside) stats.Add(data.Key);
            return stats;
        }
    }
}