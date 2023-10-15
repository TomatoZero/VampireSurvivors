using System.Collections.Generic;
using Stats.Instances;

namespace Stats.StatsCalculator
{
    public class PlayerStatCalculator : StatsCalculator
    {
        private Dictionary<Stats, float> _clearBonuses;
        private Dictionary<Stats, float> _percentBonuses;

        private HashSet<Stats> _stats;

        private delegate float GetAllBonuses(Stats stat);
        private delegate HashSet<Stats> GetAllUsingStats();
        
        protected PlayerStatCalculator(ObjectInstance objectInstance) : base(objectInstance)
        {
        }

        public override List<StatData> CalculateBonuses()
        {
            _stats = new HashSet<Stats>();
            GetClearBonuses();
            GetPercentBonuses();
            
            CombineDictionariesInFirst(_clearBonuses, _percentBonuses);
            var currentStats = new List<StatData>();

            foreach (var data in _clearBonuses)
            {
                currentStats.Add(new StatData(data.Key, data.Value, false));
            }
            
            return currentStats;
        }

        private void GetClearBonuses()
        {
            GetBonuses(_clearBonuses, GetClearBonusValue, GetClearStats);
        }

        private void GetPercentBonuses()
        {
            GetBonuses(_percentBonuses, GetAllPercentBonusValue, GetPercentBonusStats);
        }

        private void GetBonuses(Dictionary<Stats, float> dictionary, GetAllBonuses getBonuses, GetAllUsingStats getUsingStats)
        {
            dictionary = new Dictionary<Stats, float>();
            var statsInClearBonus = getUsingStats();

            foreach (var stat in statsInClearBonus)
            {
                var clear = getBonuses(stat);
                dictionary.Add(stat, clear);
            }
            
            _stats.UnionWith(statsInClearBonus);
        }
        
        private float GetAllPercentBonusValue(Stats stat)
        {
            var levelUpPercentBonus = GetValueFormDictionary(stat, LevelUpPercentBonus);
            var percentBonusFromOutside = GetValueFormDictionary(stat, PercentBonusFromOutside);
            var percentDefaultStat = GetValueFormDictionary(stat, DefaultsStatPercent);

            return levelUpPercentBonus + percentBonusFromOutside + percentDefaultStat;
        }

        private void CombineDictionariesInFirst(Dictionary<Stats, float> first, Dictionary<Stats, float> second)
        {
            foreach (var kvp in second)
            {
                if (first.ContainsKey(kvp.Key))
                {
                    first[kvp.Key] += kvp.Value;
                }
                else
                {
                    first[kvp.Key] = kvp.Value;
                }
            }
        }
        
        private HashSet<Stats> GetClearStats()
        {
            var stats = new HashSet<Stats>();

            foreach (var data in DefaultsStatClear) stats.Add(data.Key);
            foreach (var data in LevelUpClearBonus) stats.Add(data.Key);
            foreach (var data in ClearBonusFromOutside) stats.Add(data.Key);

            return stats;
        }

        private HashSet<Stats> GetPercentBonusStats()
        {
            var stats = new HashSet<Stats>();

            foreach (var data in DefaultsStatPercent) stats.Add(data.Key);
            foreach (var data in LevelUpPercentBonus) stats.Add(data.Key);
            foreach (var data in PercentBonusFromOutside) stats.Add(data.Key);

            return stats;
        }
    }
}