using Stats.Instances;

namespace Weapons.StatsController
{
    public class EnemyWeaponStatController : BaseWeaponStatController
    {
        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _setupStatData.Invoke(_instance);
            UpdateStatsEventHandler(newInstance);
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var allClearBonusFromOutside = newInstance.StatsCalculator.ClearBonuses;
            var allPercentBonusFromOutside = newInstance.StatsCalculator.PercentBonuses;

            _instance.AddBonusesFromItems(allClearBonusFromOutside, allPercentBonusFromOutside);

            _updateStatData.Invoke(_instance);
        }
    }
}