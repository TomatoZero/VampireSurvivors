using System.Collections.Generic;
using Stats.Instances;
using UnityEngine;

namespace Stats.StatsCalculators
{
    public class StatsCalculator
    {
        private Dictionary<Stats, float> _defaultsStatClear;
        private Dictionary<Stats, float> _defaultsStatPercent;

        private Dictionary<Stats, float> _levelUpClearBonus;
        private Dictionary<Stats, float> _levelUpPercentBonus;

        private Dictionary<Stats, float> _clearBonuses;
        private Dictionary<Stats, float> _percentBonuses;

        private HashSet<Stats> _stats;
        private List<StatData> _currentStats;

        private delegate float GetAllBonuses(Stats stat);

        private delegate HashSet<Stats> GetAllUsingStats();

        public Dictionary<Stats, float> ClearBonuses => _clearBonuses;
        public Dictionary<Stats, float> PercentBonuses => _percentBonuses;
        public List<StatData> CurrentStats => _currentStats;

        public StatsCalculator(ObjectInstance objectInstance)
        {
            SeparateDefaultStats(objectInstance.StatsData.DefaultStatsData);
            _levelUpClearBonus = new Dictionary<Stats, float>();
            _levelUpPercentBonus = new Dictionary<Stats, float>();
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

        public virtual void CalculateCurrentStats()
        {
            _stats = new HashSet<Stats>();
            CalculateClearBonuses();
            CalculatePercentBonuses();
            
            var currentStats = new List<StatData>();
            foreach (var stat in _stats)
            {
                var clearStat = GetValueFromDictionary(_clearBonuses, stat);
                var percentBonus = GetValueFromDictionary(_percentBonuses, stat);

                var clearPercentBonus = (clearStat * percentBonus) / 100;

                var finalValue = clearPercentBonus + clearStat;
                currentStats.Add(new StatData(stat, finalValue, false));
            }

            _currentStats = currentStats;
        }

        public virtual string ShowCurrentStats(string additionalInfo)
        {
            var str = additionalInfo + "\n";

            str += "CurrentStats:\n";
            foreach (var stat in _currentStats)
            {
                str += stat + "\n";
            }

            str += $"ClearBonuses:\n {GetDictionaryInString(_clearBonuses)}\n";
            str += $"PercentBonuses:\n {GetDictionaryInString(_percentBonuses)}\n";
            str += $"LevelUpClearBonus:\n {GetDictionaryInString(_levelUpClearBonus)}\n";
            str += $"LevelUpPercentBonus:\n {GetDictionaryInString(_levelUpPercentBonus)}\n";
            return str;
        }

        public void CalculateClearBonuses()
        {
            GetBonuses(ref _clearBonuses, GetClearBonusValue, GetClearStats);
        }

        public void CalculatePercentBonuses()
        {
            GetBonuses(ref _percentBonuses, GetAllPercentBonusValue, GetPercentBonusStats);
        }

        private protected string GetDictionaryInString<T,TT>(Dictionary<T,TT> dictionary)
        {
            var str = "";
            foreach (var stat in dictionary)
            {
                str += $"Stat: {stat.Key} Value: {stat.Value}\n";
            }

            return str;
        }
        
        private float GetValueFromDictionary(Dictionary<Stats, float> dictionary, Stats stat)
        {
            if (dictionary.TryGetValue(stat, out float value)) return value;

            return 0;
        }

        private void GetBonuses(ref Dictionary<Stats, float> dictionary, GetAllBonuses getBonuses,
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

        private protected virtual float GetAllPercentBonusValue(Stats stat)
        {
            var levelUpPercentBonus = GetValueFormDictionary(stat, _levelUpPercentBonus);
            var percentDefaultStat = GetValueFormDictionary(stat, _defaultsStatPercent);

            return levelUpPercentBonus + percentDefaultStat;
        }

        private protected virtual float GetClearBonusValue(Stats stat)
        {
            var levelUpClearBonus = GetValueFormDictionary(stat, _levelUpClearBonus);
            var defaultClearStat = GetValueFormDictionary(stat, _defaultsStatClear);

            return levelUpClearBonus + defaultClearStat;
        }

        private protected virtual HashSet<Stats> GetClearStats()
        {
            var stats = new HashSet<Stats>();

            foreach (var data in _defaultsStatClear) stats.Add(data.Key);
            foreach (var data in _levelUpClearBonus) stats.Add(data.Key);

            return stats;
        }

        private protected virtual HashSet<Stats> GetPercentBonusStats()
        {
            var stats = new HashSet<Stats>();

            foreach (var data in _defaultsStatPercent) stats.Add(data.Key);
            foreach (var data in _levelUpPercentBonus) stats.Add(data.Key);

            return stats;
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
            _defaultsStatPercent = new Dictionary<Stats, float>();
            _defaultsStatClear = new Dictionary<Stats, float>();
            
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