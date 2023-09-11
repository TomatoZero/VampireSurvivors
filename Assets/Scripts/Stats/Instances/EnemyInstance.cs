using System.Collections.Generic;
using Stats.ScriptableObjects;

namespace Stats.Instances
{
    public class EnemyInstance : ObjectInstance
    {
        public List<StatData> CurrentStats => _currentStats;

        public EnemyInstance(EnemyStatsData statsData) : base(statsData)
        {
        }
    }
}