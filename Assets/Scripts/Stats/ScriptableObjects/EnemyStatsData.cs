using UnityEngine;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyStatsData", menuName = "ScriptableObject/Stats/Enemy", order = 1)]
    public class EnemyStatsData : ObjectStatsData
    {
        [SerializeField] private string _name;
    }
}