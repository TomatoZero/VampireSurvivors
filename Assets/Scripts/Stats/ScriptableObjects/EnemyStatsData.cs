using System.Collections.Generic;
using UnityEngine;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyStatsData", menuName = "ScriptableObject/Stats/Enemy", order = 1)]
    public class EnemyStatsData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private List<StatData> _stats;

        public List<StatData> Stats => _stats;
    }
}