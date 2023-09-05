using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class PlayerStatsInstance : ObjectStatsInstance
    {
        private int _lvl;
        public List<StatData> CurrentStats => _currentStats;

        public PlayerStatsInstance(PlayerStatsData playerStatsData) : base(playerStatsData)
        {
        }
    }
}