using System;
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

        public override void LevelUp()
        {
            throw new Exception("Enemy shouldn't have level up");
        }
    }
}