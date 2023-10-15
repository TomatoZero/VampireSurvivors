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


        public Dictionary<Stats, float> DefaultsStatClear => _defaultsStatClear;
        public Dictionary<Stats, float> DefaultsStatPercent => _defaultsStatPercent;
        public Dictionary<Stats, float> LevelUpClearBonus => _levelUpClearBonus;
        public Dictionary<Stats, float> LevelUpPercentBonus => _levelUpPercentBonus;
        public Dictionary<Stats, float> ClearBonusFromOutside => _clearBonusFromOutside;
        public Dictionary<Stats, float> PercentBonusFromOutside => _percentBonusFromOutside;
        

        protected StatsCalculator(ObjectInstance objectInstance)
        {
            SeparateDefaultStats(objectInstance.StatsData.DefaultStatsData);
            _levelUpClearBonus = new Dictionary<Stats, float>();
            _levelUpPercentBonus = new Dictionary<Stats, float>();

            _clearBonusFromOutside = new Dictionary<Stats, float>();
            _percentBonusFromOutside = new Dictionary<Stats, float>();
        }

        public virtual List<StatData> CalculateBonuses()
        {
            var allUsingStats = GetAllUsingStat();

            var currentStats = new List<StatData>();
            foreach (var stat in allUsingStats)
            {
                var currentStatsValue = GetFinalStat(stat);
                currentStats.Add(new StatData(stat, currentStatsValue, false));
            }

            return currentStats;
        }

        private protected virtual float GetFinalStat(Stats stat)
        {
            var clearStat = GetClearBonusValue(stat);
            var percentBonusValue = GetPercentBonusValue(stat, clearStat);

            return clearStat + percentBonusValue;
        }

        private protected virtual float GetClearBonusValue(Stats stat)
        {
            var levelUpClearBonus = GetValueFormDictionary(stat, _levelUpClearBonus);
            var clearBonusFromOutside = GetValueFormDictionary(stat, _clearBonusFromOutside);
            var defaultClearStat = GetValueFormDictionary(stat, _defaultsStatClear);

            return levelUpClearBonus + clearBonusFromOutside + defaultClearStat;
        }

        private protected virtual float GetPercentBonusValue(Stats stat, float clearStat)
        {
            var levelUpPercentBonus = GetValueFormDictionary(stat, _levelUpPercentBonus);
            var percentBonusFromOutside = GetValueFormDictionary(stat, _percentBonusFromOutside);
            var percentDefaultStat = GetValueFormDictionary(stat, _defaultsStatPercent);

            var percentSum = levelUpPercentBonus + percentBonusFromOutside + percentDefaultStat;

            var percentBonus = (clearStat * percentSum) / 100;
            return percentBonus;
        }

        private protected float GetValueFromList(Stats stat, List<StatData> list)
        {
            foreach (var statData in list)
            {
                if (statData.Stat == stat)
                    return statData.Value;
            }

            return 0;
        }

        private protected float GetValueFormDictionary(Stats stats, Dictionary<Stats, float> dictionary)
        {
            if (dictionary.TryGetValue(stats, out var value))
            {
                return value;
            }

            return 0;
        }

        private HashSet<Stats> GetAllUsingStat()
        {
            var usingStat = new HashSet<Stats>();

            foreach (var data in _defaultsStatClear) usingStat.Add(data.Key);
            foreach (var data in _defaultsStatPercent) usingStat.Add(data.Key);

            foreach (var data in _levelUpClearBonus) usingStat.Add(data.Key);
            foreach (var data in _clearBonusFromOutside) usingStat.Add(data.Key);

            foreach (var data in _levelUpPercentBonus) usingStat.Add(data.Key);
            foreach (var data in _percentBonusFromOutside) usingStat.Add(data.Key);

            return usingStat;
        }

        private void SeparateDefaultStats(List<StatData> defaultStat)
        {
            foreach (var statData in defaultStat)
            {
                if (statData.IsPercent)
                {
                    _defaultsStatPercent.Add(statData.Stat, statData.Value);
                }
                else
                {
                    _defaultsStatClear.Add(statData.Stat, statData.Value);
                }
            }
        }
    }
}