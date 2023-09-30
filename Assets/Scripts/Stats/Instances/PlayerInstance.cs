using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class PlayerInstance : PowerUpInstance
    {
        public PlayerStatsData PlayerStatsData => (PlayerStatsData)_statsData;

        public PlayerInstance(PlayerStatsData playerStatsData) : base(playerStatsData)
        {
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
                case Stats.Recovery:
                    newValue = CalculateNewValue(defaultValue, bonusValue);
                    SetStatByName(stat, newValue);
                    break;
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