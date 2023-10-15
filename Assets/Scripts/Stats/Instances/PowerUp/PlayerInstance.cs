using System.Collections.Generic;
using Stats.ScriptableObjects;
using Stats.StatsCalculators;

namespace Stats.Instances.PowerUp
{
    public class PlayerInstance : PowerUpInstance
    {
        public PlayerStatsData PlayerStatsData => (PlayerStatsData)_statsData;
        public PlayerStatCalculator PlayerStatCalculator => (PlayerStatCalculator)StatsCalculator;


        public PlayerInstance(PlayerStatsData playerStatsData, List<StatData> bonusFromItems) : base(playerStatsData)
        {
            AddBonusesFromItem(bonusFromItems);
        }

        private protected override void Setup()
        {
            var playerStatCalculator = new PlayerStatCalculator(this);
            _currentStats = playerStatCalculator.CalculateCurrentStats();
            SetStatCalculator(playerStatCalculator);
        }

        public virtual void AddBonusesFromItem(List<StatData> bonusFromItems)
        {
            foreach (var bonusFromItem in bonusFromItems)
            {
                PowerUpStatCalculator.RewriteOrAddOutsideBonus(bonusFromItem);
            }
        }
        
        public override void LevelUp()
        {
            if (_statsData.MaxLvl <= CurrentLvl) return;

            if ((CurrentLvl + 1) % 10 == 0)
            {
                var lvlUpStatsData = _statsData.LevelUpBonuses[CurrentLvl - 1];

                foreach (var statData in lvlUpStatsData.BonusStat)
                {
                    PowerUpStatCalculator.AddLevelUpBonus(statData);
                }
            }

            IncreaseLevel();
        }
    }
}