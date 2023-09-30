using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class ItemInstance : ObjectInstance
    {
        public ItemInstance(ObjectStatsData statsData) : base(statsData)
        {
            
        }
        
        public override void LevelUp()
        {
            if(_statsData.MaxLvl >= _currentLvl) return;
            
            var lvlUpStatsData = _statsData.LevelUpBonuses[_currentLvl - 1];

            foreach (var statData in lvlUpStatsData.BonusStat)
            {
                var currentValue = GetStatByName(statData.Stat).Value;
                SetStatByName(statData.Stat, currentValue + statData.Value);
            }
            
            _currentLvl++;
        }
    }
}