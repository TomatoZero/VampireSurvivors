using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponStatsData", menuName = "ScriptableObject/Stats/Weapon", order = 2)]
    public class WeaponStatsData : ObjectStatsData
    {
        [SerializeField] private List<Stats.Stats> _ignoreStat;
        
        public List<Stats.Stats> IgnoreStat => _ignoreStat;
    }
}