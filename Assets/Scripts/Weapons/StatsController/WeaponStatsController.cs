using System.Collections.Generic;
using Stats.Instances;
using Stats.Instances.PowerUp;

namespace Weapons.StatsController
{
    public class WeaponStatsController : BaseWeaponStatController
    {
        public override void SetupStatEventHandler(ObjectInstance playerInstance)
        {
            _setupStatData.Invoke(_instance);
            UpdateStatsEventHandler(playerInstance);
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var playerInstance = (PlayerInstance)newInstance;

            var allClearBonusFromOutside = playerInstance.PlayerStatCalculator.ClearBonuses;
            var allPercentBonusFromOutside = playerInstance.PlayerStatCalculator.PercentBonuses;

            _instance.AddBonusesFromItems(allClearBonusFromOutside, allPercentBonusFromOutside);

            _updateStatData.Invoke(_instance);
        }

        private string GetDict(Dictionary<Stats.Stats, float> dictionary)
        {
            var str = "Percent";
            foreach (var bonus in dictionary)
            {
                str += $"{bonus.Key} {bonus.Value}\n";
            }

            return str;
        }
    }
}