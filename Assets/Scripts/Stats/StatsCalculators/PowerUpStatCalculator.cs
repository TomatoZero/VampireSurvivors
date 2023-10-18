using System.Collections.Generic;
using Stats.Instances;

namespace Stats.StatsCalculators
{
    public class PowerUpStatCalculator : StatsCalculator
    {
        private Dictionary<Stats, float> _clearBonusFromOutside;
        private Dictionary<Stats, float> _percentBonusFromOutside;

        private protected Dictionary<Stats, float> ClearBonusFromOutside
        {
            get => _clearBonusFromOutside;
            set => _clearBonusFromOutside = value;
        }

        private protected Dictionary<Stats, float> PercentBonusFromOutside
        {
            get => _percentBonusFromOutside;
            set => _percentBonusFromOutside = value;
        }

        public PowerUpStatCalculator(ObjectInstance objectInstance) : base(objectInstance)
        {
            _clearBonusFromOutside = new Dictionary<Stats, float>();
            _percentBonusFromOutside = new Dictionary<Stats, float>();
        }

        public virtual void RewriteOrAddOutsideBonus(Dictionary<Stats, float> allClearItemBonus,
            Dictionary<Stats, float> allPercentItemBonus)
        {
            _percentBonusFromOutside = new Dictionary<Stats, float>();
            _clearBonusFromOutside = new Dictionary<Stats, float>();

            foreach (var bonus in allClearItemBonus)
                _clearBonusFromOutside[bonus.Key] = bonus.Value;
            
            foreach (var bonus in allPercentItemBonus)
                _percentBonusFromOutside[bonus.Key] = bonus.Value;
        }

        public override string ShowCurrentStats(string additionalInfo)
        {
            var str = base.ShowCurrentStats(additionalInfo);

            str += $"ClearBonusFromOutside {GetDictionaryInString(_clearBonusFromOutside)}\n\n";
            str += $"PercentBonusFromOutside {GetDictionaryInString(_percentBonusFromOutside)}\n\n";
            
            return str;
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