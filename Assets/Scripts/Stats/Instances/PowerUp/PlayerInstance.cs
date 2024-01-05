using System.Collections.Generic;
using ScriptableObjects;
using Stats.StatsCalculators;
using UnityEngine;

namespace Stats.Instances.PowerUp
{
    public class PlayerInstance : PowerUpInstance
    {
        public PlayerStatsData PlayerStatsData => (PlayerStatsData)_statsData;
        public UnitStatCalculator UnitStatCalculator => (UnitStatCalculator)StatsCalculator;


        public PlayerInstance(PlayerStatsData playerStatsData, Dictionary<Stats, float> allClearItemBonus,
            Dictionary<Stats, float> allPercentItemBonus) : base(playerStatsData)
        {
            AddBonusesFromItems(allClearItemBonus, allPercentItemBonus);
        }

        public override void AddBonusesFromItems(Dictionary<Stats, float> allClearItemBonus,
            Dictionary<Stats, float> allPercentItemBonus)
        {
            UnitStatCalculator.RewriteOrAddOutsideBonus(allClearItemBonus, allPercentItemBonus);
            UpdateCurrentStats();
        }

        public void AddBuffs(Dictionary<Stats, float> allClearBuff,
            Dictionary<Stats, float> allPercentBuff)
        {
            UnitStatCalculator.RewriteOrAddBuffs(allClearBuff, allPercentBuff);
            UpdateCurrentStats();
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

        private protected override void Setup()
        {
            var playerStatCalculator = new UnitStatCalculator(this);
            playerStatCalculator.CalculateCurrentStats();
            SetStatCalculator(playerStatCalculator);
        }
    }
}