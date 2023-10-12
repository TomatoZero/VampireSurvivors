using System.Collections.Generic;
using Stats.ScriptableObjects;
using UnityEngine;

namespace Stats.Instances
{
    public abstract class PowerUpInstance : ObjectInstance
    {
        private List<StatData> _currentBonus;
        private List<StatData> _levelUpBonus;
        private List<StatData> _outsideBonuses;

        public List<StatData> CurrentStats
        {
            get => _currentStats;
            private protected set => _currentStats = value;
        }

        public List<StatData> CurrentBonus
        {
            get => _currentBonus;
            private protected set => _currentBonus = value;
        }

        protected PowerUpInstance(ObjectStatsData statsData) : base(statsData)
        {
        }

        protected override void SetupStat()
        {
            _currentBonus = new List<StatData>();
            _levelUpBonus = new List<StatData>();
            _outsideBonuses = new List<StatData>();

            base.SetupStat();
        }

        public override void LevelUp()
        {
            if (_statsData.MaxLvl <= _currentLvl) return;

            var lvlUpStatsData = _statsData.LevelUpBonuses[_currentLvl - 1];
            foreach (var statData in lvlUpStatsData.BonusStat)
            {
                AddValueToLevelUpBonus(statData.Stat, statData.Value);
            }

            _currentLvl++;
        }

        public virtual void AddValueToBonus(Stats stat, float addValue)
        {
            foreach (var bonus in _outsideBonuses)
            {
                if (bonus.Stat == stat)
                {
                    bonus.Value = addValue;

                    var levelUp = GetBonusValue(_levelUpBonus, stat);
                    var currentStat = GetStatFromCurrenBonus(stat);

                    currentStat.Value = bonus.Value + levelUp;

                    UpdateStatWithBonus(stat, currentStat.Value);
                    return;
                }
            }

            if (!IsNecessaryStat(stat)) return;
            SetValueWithLevelUpBonuses(stat, addValue);
        }

        private protected virtual void AddValueToLevelUpBonus(Stats stat, float addValue)
        {
            foreach (var bonus in _levelUpBonus)
            {
                if (bonus.Stat == stat)
                {
                    bonus.Value += addValue;
                    var outsideBonusValue = GetBonusValue(_outsideBonuses, stat);
                    var currentStat = GetStatFromCurrenBonus(stat);

                    currentStat.Value = bonus.Value + outsideBonusValue;

                    UpdateStatWithBonus(stat, currentStat.Value);
                    return;
                }
            }

            SetValueWithOutsideBonuses(stat, addValue);
        }

        protected virtual StatData GetStatFromCurrenBonus(Stats stats)
        {
            var s = "";
            foreach (var bonus in CurrentBonus)
            {
                s += bonus + "\n";
                if (bonus.Stat == stats) return bonus;
            }

            Debug.Log(s);

            return new StatData();
        }

        private protected virtual void UpdateStatWithBonus(Stats stat, float bonusValue)
        {
            var defaultValue = GetDefaultStatByName(stat).Value;
            UpdateStatWithBonus(stat, bonusValue, defaultValue);
        }

        private float GetBonusValue(List<StatData> list, Stats stat)
        {
            foreach (var outsideBonus in list)
            {
                if (outsideBonus.Stat == stat) return outsideBonus.Value;
            }

            return 0f;
        }

        private void SetValueWithLevelUpBonuses(Stats stats, float value)
        {
            SetValueInAnotherLists(_outsideBonuses, _levelUpBonus, stats, value);
        }

        private void SetValueWithOutsideBonuses(Stats stats, float value)
        {
            SetValueInAnotherLists(_levelUpBonus, _outsideBonuses, stats, value);
        }

        private void SetValueInAnotherLists(List<StatData> listWithoutBonus, List<StatData> listWithSecondPart,
            Stats stat, float value)
        {
            listWithoutBonus.Add(new StatData(stat, value));

            var statFromCurrent = GetStatFromCurrenBonus(stat);

            if (statFromCurrent.Stat != Stats.MaxHealth && statFromCurrent.Value != 0)
            {
                var levelUp = GetBonusValue(listWithSecondPart, stat);
                statFromCurrent.Value = levelUp + value;
            }
            else
            {
                CurrentBonus.Add(new StatData(stat, value));
            }

            UpdateStatWithBonus(stat, value);
        }

        private protected abstract void UpdateStatWithBonus(Stats stat, float bonusValue, float defaultValue);
        private protected abstract bool IsNecessaryStat(Stats statData);
    }
}