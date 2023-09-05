using Stats.Instances;
using Stats.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyStatsController : MonoBehaviour
    {
        [SerializeField] private EnemyStatsData _enemyStatsData;
        [SerializeField] private UnityEvent<EnemyStatsInstance> _statsUpdateEvent;

        private EnemyStatsInstance _statsInstance;

        private void Awake()
        {
            _statsInstance = new EnemyStatsInstance(_enemyStatsData);
            _statsUpdateEvent.Invoke(_statsInstance);
        }
    }
}