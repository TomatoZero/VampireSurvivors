using Stats;
using Stats.ScriptableObjects;
using UnityEngine;

namespace UI.Structs
{
    public struct BonusData
    {
        private readonly Sprite _ico;
        private readonly string _name;
        private readonly string _level;
        private readonly string _description;
        private readonly bool _isWeapon;

        public Sprite Ico => _ico;
        public string Name => _name;
        public string Level => _level;
        public string Description => _description;
        public bool IsWeapon => _isWeapon;

        public BonusData(ObjectStatsData data, int newLevel)
        {
            _ico = data.Ico;
            _name = data.Name;
            _level = $"New level {newLevel}";
            _description = "";
            
            _isWeapon = (data is WeaponStatsData);

            if (newLevel == 0) _description = $"New weapon {data.Name}";
            else _description = ConvertStatDataToDescription(data.LevelUpBonuses[newLevel - 1]);
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