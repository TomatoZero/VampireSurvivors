using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct LevelUpBonuses
    {
        [SerializeField] private List<StatData> _bonusStat;

        public List<StatData> BonusStat => _bonusStat;
    }
}