using System.Collections.Generic;
using Stats.Instances;

namespace Stats.StatsCalculator
{
    public class StatsCalculator
    {
        private Dictionary<Stats, float> _defaultsStatClear;
        private Dictionary<Stats, float> _defaultsStatPercent;

        private Dictionary<Stats, float> _levelUpClearBonus;
        private Dictionary<Stats, float> _levelUpPercentBonus;

        private Dictionary<Stats, float> _clearBonusFromOutside;
        private Dictionary<Stats, float> _percentBonusFromOutside;

        private Dictionary<Stats, float> _clearBonuses;
        private Dictionary<Stats, float> _percentBonuses;

        private HashSet<Stats> _stats;

        private delegate float GetAllBonuses(Stats stat);

        private delegate HashSet<Stats> GetAllUsingStats();

        // public Dictionary<Stats, float> DefaultsStatClear => _defaultsStatClear;
        // public Dictionary<Stats, float> DefaultsStatPercent => _defaultsStatPercent;
        //
        // public Dictionary<Stats, float> LevelUpClearBonus => _levelUpClearBonus;
        // public Dictionary<Stats, float> LevelUpPercentBonus => _levelUpPercentBonus;
        //
        // public Dictionary<Stats, float> ClearBonusFromOutside => _clearBonusFromOutside;
        // public Dictionary<Stats, float> PercentBonusFromOutside => _percentBonusFromOutside;


        protected StatsCalculator(ObjectInstance objectInstance)
        {
            SeparateDefaultStats(objectInstance.StatsData.DefaultStatsData);
            _levelUpClearBonus = new Dictionary<Stats, float>();
            _levelUpPercentBonus = new Dictionary<Stats, float>();

            _clearBonusFromOutside = new Dictionary<Stats, float>();
            _percentBonusFromOutside = new Dictionary<Stats, float>();
        }

        public void AddLevelUpBonus(StatData statData)
        {
            if (statData.IsPercent)
            {
                AddValueInDictionary(_levelUpPercentBonus, statData);
            }
            else
            {
                AddValueInDictionary(_levelUpClearBonus, statData);
            }
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

        public virtual List<StatData> CalculateBonuses()
        {
            _stats = new HashSet<Stats>();
            GetClearBonuses();
            GetPercentBonuses();

            var currentStats = new List<StatData>();
            foreach (var stat in _stats)
            {
                var clearStat = GetValueFromDictionary(_clearBonuses, stat);
                var percentBonus = GetValueFromDictionary(_percentBonuses, stat);

                var clearPercentBonus = (clearStat * percentBonus) / 100;

                var finalValue = clearPercentBonus + clearStat;
                currentStats.Add(new StatData(stat, finalValue, false));
            }

            return currentStats;
        }

        private float GetValueFromDictionary(Dictionary<Stats, float> dictionary, Stats stat)
        {
            if (dictionary.TryGetValue(stat, out float value)) return value;

            return 0;
        }

        private void GetClearBonuses()
        {
            GetBonuses(_clearBonuses, GetClearBonusValue, GetClearStats);
        }

        private void GetPercentBonuses()
        {
            GetBonuses(_percentBonuses, GetAllPercentBonusValue, GetPercentBonusStats);
        }

        private void GetBonuses(Dictionary<Stats, float> dictionary, GetAllBonuses getBonuses,
            GetAllUsingStats getUsingStats)
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
            var levelUpPercentBonus = GetValueFormDictionary(stat, _levelUpPercentBonus);
            var percentBonusFromOutside = GetValueFormDictionary(stat, _percentBonusFromOutside);
            var percentDefaultStat = GetValueFormDictionary(stat, _defaultsStatPercent);

            return levelUpPercentBonus + percentBonusFromOutside + percentDefaultStat;
        }

        private HashSet<Stats> GetClearStats()
        {
            var stats = new HashSet<Stats>();

            foreach (var data in _defaultsStatClear) stats.Add(data.Key);
            foreach (var data in _levelUpClearBonus) stats.Add(data.Key);
            foreach (var data in _clearBonusFromOutside) stats.Add(data.Key);

            return stats;
        }

        private HashSet<Stats> GetPercentBonusStats()
        {
            var stats = new HashSet<Stats>();

            foreach (var data in _defaultsStatPercent) stats.Add(data.Key);
            foreach (var data in _levelUpPercentBonus) stats.Add(data.Key);
            foreach (var data in _percentBonusFromOutside) stats.Add(data.Key);

            return stats;
        }

        private protected virtual float GetClearBonusValue(Stats stat)
        {
            var levelUpClearBonus = GetValueFormDictionary(stat, _levelUpClearBonus);
            var clearBonusFromOutside = GetValueFormDictionary(stat, _clearBonusFromOutside);
            var defaultClearStat = GetValueFormDictionary(stat, _defaultsStatClear);

            return levelUpClearBonus + clearBonusFromOutside + defaultClearStat;
        }

        private protected float GetValueFormDictionary(Stats stats, Dictionary<Stats, float> dictionary)
        {
            if (dictionary.TryGetValue(stats, out var value))
            {
                return value;
            }

            return 0;
        }

        private protected void AddValueInDictionary(Dictionary<Stats, float> dictionary, StatData data)
        {
            if (dictionary.ContainsKey(data.Stat))
                dictionary[data.Stat] += data.Value;
            else
                dictionary[data.Stat] = data.Value;
        }

        private void SeparateDefaultStats(List<StatData> defaultStat)
        {
            foreach (var statData in defaultStat)
            {
                if (statData.IsPercent)
                    _defaultsStatPercent.Add(statData.Stat, statData.Value);
                else
                    _defaultsStatClear.Add(statData.Stat, statData.Value);
            }
        }
    }
}