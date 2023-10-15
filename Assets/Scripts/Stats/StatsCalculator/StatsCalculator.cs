using System.Collections.Generic;
using Stats.Instances;

namespace Stats.StatsCalculator
{
    public class StatsCalculator
    {
        private ObjectInstance _objectInstance;
        
        private List<StatData> _levelUpClearBonus;
        private List<StatData> _levelUpPercentBonus;

        private List<StatData> _clearBonusFromOutside;
        private List<StatData> _percentBonusFromOutside;

        
        protected StatsCalculator(ObjectInstance objectInstance)
        {
            _objectInstance = objectInstance;
            
            _levelUpClearBonus = new List<StatData>();
            _levelUpPercentBonus = new List<StatData>();

            _clearBonusFromOutside = new List<StatData>();
            _percentBonusFromOutside = new List<StatData>();
        }

        public List<StatData> CalculateBonuses()
        {
            var allUsingStats = GetAllUsingStat();

            var currentStats = new List<StatData>();
            foreach (var stat in allUsingStats)
            {
                var currentStatsValue = GetFinalStat(stat);
                currentStats.Add(new StatData(stat, currentStatsValue));
            }

            return currentStats;
        }

        private protected virtual float GetFinalStat(Stats stat)
        {
            var clearBonus = GetClearBonusValue(stat);
            var clearStat = clearBonus + _objectInstance.GetDefaultStatByName(stat).Value;
            var percentBonusValue = GetBonusValueFromPercent(stat, clearStat);

            return clearStat + percentBonusValue;
        }
        
        private protected virtual float GetClearBonusValue(Stats stat)
        {
            var levelUpClearBonus = GetValueFromList(stat, _levelUpClearBonus);
            var clearBonusFromOutside = GetValueFromList(stat, _clearBonusFromOutside);

            return levelUpClearBonus + clearBonusFromOutside;
        }

        private protected virtual float GetBonusValueFromPercent(Stats stat, float clearStat)
        {
            var levelUpPercentBonus = GetValueFromList(stat, _levelUpPercentBonus);
            var percentBonusFromOutside = GetValueFromList(stat, _percentBonusFromOutside);

            var percentBonus = (clearStat * (levelUpPercentBonus + percentBonusFromOutside)) / 100;
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

        private HashSet<Stats> GetAllUsingStat()
        {
            var usingStat = new HashSet<Stats>();

            foreach (var data in _objectInstance.StatsData.DefaultStatsData)
            {
                usingStat.Add(data.Stat);
            }
            
            foreach (var data in _levelUpClearBonus) usingStat.Add(data.Stat);
            foreach (var data in _clearBonusFromOutside) usingStat.Add(data.Stat);
            foreach (var data in _levelUpPercentBonus) usingStat.Add(data.Stat);
            foreach (var data in _percentBonusFromOutside) usingStat.Add(data.Stat);
            
            return usingStat;
        }
    }
}