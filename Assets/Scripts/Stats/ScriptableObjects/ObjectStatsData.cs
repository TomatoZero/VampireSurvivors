﻿using System.Collections.Generic;
using UnityEngine;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "ScriptableObject/Stats/StatsData", order = 3)]
    public class ObjectStatsData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private List<StatData> _stats;

        public List<StatData> Stats => _stats;

        public string Name => _name;
    }
}