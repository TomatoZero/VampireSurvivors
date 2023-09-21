using System.Collections.Generic;
using UnityEngine;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerStatsData", menuName = "ScriptableObject/Stats/Player", order = 0)]
    public class PlayerStatsData : ObjectStatsData
    {
        [SerializeField] private List<StatData> _bonusStats;
        
        public List<StatData> BonusStats => _bonusStats;
    }
}