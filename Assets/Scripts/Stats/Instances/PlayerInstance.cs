using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class PlayerInstance : PowerUpInstance
    {
        public PlayerStatsData PlayerStatsData => (PlayerStatsData)_statsData;

        public PlayerInstance(PlayerStatsData playerStatsData, List<StatData> bonusFromItems) : base(playerStatsData)
        {
            foreach (var bonusFromItem in bonusFromItems)
            {
                AddValueToBonus(bonusFromItem.Stat, bonusFromItem.Value);
            }
        }

        public virtual void AddBonusesFromItem(List<StatData> bonusFromItems)
        {
            foreach (var bonusFromItem in bonusFromItems)
            {
                AddValueToBonus(bonusFromItem.Stat, bonusFromItem.Value);
            }
        }
        
        public override void LevelUp()
        {
            if (_statsData.MaxLvl >= _currentLvl) return;

            if ((_currentLvl + 1) % 10 == 0)
            {
                var lvlUpStatsData = _statsData.LevelUpBonuses[_currentLvl - 1];

                foreach (var statData in lvlUpStatsData.BonusStat)
                {
                    AddValueToBonus(statData.Stat, statData.Value);
                }
            }

            _currentLvl++;
        }

        protected override void SetupStat()
        {
            base.SetupStat();
            CurrentBonus = new List<StatData>();

            foreach (var bonus in PlayerStatsData.BonusStats)
            {
                CurrentBonus.Add((StatData)bonus.Clone());
            }

            foreach (var currentBonus in CurrentBonus)
            {
                UpdateStatWithBonus(currentBonus.Stat, currentBonus.Value);
            }
        }

        private protected override void UpdateStatWithBonus(Stats stat, float bonusValue, float defaultValue)
        {
            float newValue = 0;
            switch (stat)
            {
                case Stats.MaxHealth:
                case Stats.MoveSpeed:
                    newValue = CalculateNewValue(defaultValue, bonusValue);
                    SetStatByName(stat, newValue);
                    break;
                case Stats.Recovery:
                case Stats.Armor:
                case Stats.Luck:
                case Stats.Growth:
                case Stats.Greed:
                case Stats.Curse:
                    newValue = defaultValue + bonusValue;
                    SetStatByName(stat, newValue);
                    break;
            }
        }
    }
}