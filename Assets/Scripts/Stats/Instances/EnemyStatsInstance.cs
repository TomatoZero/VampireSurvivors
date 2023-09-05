using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class EnemyStatsInstance : ObjectStatsInstance
    {
        public List<StatData> CurrentStats => _currentStats;

        public EnemyStatsInstance(EnemyStatsData statsData) : base(statsData)
        {
        }
    }
}