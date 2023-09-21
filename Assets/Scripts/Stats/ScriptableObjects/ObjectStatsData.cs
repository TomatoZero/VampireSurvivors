using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "ScriptableObject/Stats/StatsData", order = 3)]
    public class ObjectStatsData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private List<StatData> _defaultStatsData;
        
        public string Name => _name;

        public List<StatData> DefaultStatsData => _defaultStatsData;
    }
}