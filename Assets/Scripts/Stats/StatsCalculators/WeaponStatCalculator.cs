using System.Collections.Generic;
using Stats.Instances;
using Stats.ScriptableObjects;

namespace Stats.StatsCalculators
{
    public class WeaponStatCalculator : PowerUpStatCalculator
    {
        private HashSet<Stats> _ignoreStats;

        public WeaponStatCalculator(ObjectInstance objectInstance, WeaponStatsData weaponData) : base(objectInstance)
        {
            _ignoreStats = new HashSet<Stats>();
            
            foreach (var stat in weaponData.IgnoreStat)
            {
                _ignoreStats.Add(stat);
            }
        }

        public override void RewriteOrAddOutsideBonus(Dictionary<Stats, float> allClearItemBonus,
            Dictionary<Stats, float> allPercentItemBonus)
        {
            ClearBonusFromOutside = new Dictionary<Stats, float>();
            PercentBonusFromOutside = new Dictionary<Stats, float>();

            foreach (var bonus in allClearItemBonus)
            {
                if(_ignoreStats.Contains(bonus.Key)) continue;
                ClearBonusFromOutside[bonus.Key] = bonus.Value;
            }

            foreach (var bonus in allPercentItemBonus)
            {
                if(_ignoreStats.Contains(bonus.Key)) continue;
                PercentBonusFromOutside[bonus.Key] = bonus.Value;
            }
        }
    }
}