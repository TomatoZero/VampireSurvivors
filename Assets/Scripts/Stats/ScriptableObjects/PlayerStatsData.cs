using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerStatsData", menuName = "ScriptableObject/Stats/Player", order = 0)]
    public class PlayerStatsData : ScriptableObject
    {
        [FormerlySerializedAs("_playerStats")] [SerializeField] private List<StatData> _stats;

        public List<StatData> Stats => _stats;
    }
}