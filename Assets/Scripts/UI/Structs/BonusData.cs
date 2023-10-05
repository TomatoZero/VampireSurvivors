using Stats;
using Stats.ScriptableObjects;

namespace UI.Structs
{
    public struct BonusData
    {
        private readonly ObjectStatsData _statsData;
        private readonly int _level;
        private readonly bool _isWeapon;

        public ObjectStatsData StatsData => _statsData;

        public int Level => _level;
        public bool IsWeapon => _isWeapon;

        public BonusData(ObjectStatsData data, int newLevel)
        {
            _statsData = data;
            _level = newLevel;
            _isWeapon = (data is WeaponStatsData);
        }

        private string ConvertStatDataToDescription(LevelUpBonuses stat)
        {
            var description = "";

            foreach (var statData in stat.BonusStat)
            {
                description += $"Add {statData.Value} to {statData.Stat}";
            }

            return description;
        }
    }
}