﻿using Stats.Instances;

namespace Interface
{
    public interface IUpdateStats
    {
        public void SetupStatEventHandler(ObjectInstance newInstance);
        public void UpdateStatsEventHandler(ObjectInstance newInstance);
    }
}