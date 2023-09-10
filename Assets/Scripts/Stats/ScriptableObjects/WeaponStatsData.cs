using System.Collections.Generic;
using UnityEngine;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponStatsData", menuName = "ScriptableObject/Stats/Weapon", order = 2)]
    public class WeaponStatsData : ObjectStatsData
    {
        [SerializeField] private List<Stats> _ignoreStat;
    }
}