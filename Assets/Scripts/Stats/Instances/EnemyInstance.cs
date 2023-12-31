﻿using System;
using ScriptableObjects;
using Stats.StatsCalculators;

namespace Stats.Instances
{
    public class EnemyInstance : ObjectInstance
    {
        public EnemyInstance(EnemyStatsData statsData) : base(statsData)
        {
        }

        private protected override void Setup()
        {
            var statCalculator = new StatsCalculator(this);
            statCalculator.CalculateCurrentStats();
            SetStatCalculator(statCalculator);
        }

        //TODO: fix this part. SOLID break. Liskov principle  
        public override void LevelUp()
        {
            throw new Exception("Enemy shouldn't have level up");
        }
    }
}