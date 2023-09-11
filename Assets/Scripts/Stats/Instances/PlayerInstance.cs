using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class PlayerInstance : ObjectInstance
    {
        private int _lvl;
        public List<StatData> CurrentStats => _currentStats;

        public PlayerInstance(PlayerStatsData playerStatsData) : base(playerStatsData)
        {
        }
    }
}