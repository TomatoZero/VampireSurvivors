using System.Collections.Generic;
using Stats.Instances;

namespace Stats.StatsCalculators
{
    public class UnitStatCalculator : PowerUpStatCalculator
    {
        private Dictionary<Stats, float> _clearBuffs;
        private Dictionary<Stats, float> _percentBuffs;

        public UnitStatCalculator(ObjectInstance objectInstance) : base(objectInstance)
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

        private protected override float GetClearBonusValue(Stats stat)
        {
            var clearBuff = GetValueFormDictionary(stat, _clearBuffs);
            return base.GetClearBonusValue(stat) + clearBuff;
        }

        private protected override float GetPercentBonusValue(Stats stat)
        {
            var percentBuff = GetValueFormDictionary(stat, _percentBuffs);
            return base.GetPercentBonusValue(stat) + percentBuff;
        }

        private protected override HashSet<Stats> GetClearStats()
        {
            var stats = base.GetClearStats();

            foreach (var data in _clearBuffs) stats.Add(data.Key);

            return stats;
        }

        private protected override HashSet<Stats> GetPercentStats()
        {
            var stats = base.GetPercentStats();

            foreach (var data in _percentBuffs) stats.Add(data.Key);

            return stats;
        }

        public override string ShowCurrentStats(string additionalInfo)
        {
            var str = base.ShowCurrentStats(additionalInfo);

            str += $"ClearBuff {GetDictionaryInString(_clearBuffs)}\n\n";
            str += $"PercentBuff {GetDictionaryInString(_percentBuffs)}\n\n";

            return str;
        }
    }
}