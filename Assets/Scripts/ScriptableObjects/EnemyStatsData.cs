using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyStatData", menuName = "ScriptableObject/Stats/EnemyRework", order = 0)]
    public class EnemyStatsData : ObjectStatsData
    {
        [SerializeField] private List<AnimatorController> _levelEvolution;

        public List<AnimatorController> LevelEvolution => _levelEvolution;
    }
}