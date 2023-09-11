using Stats.Instances;
using Stats.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyStatsController : MonoBehaviour
    {
        [SerializeField] private EnemyStatsData _enemyStatsData;
        [SerializeField] private UnityEvent<EnemyInstance> _statsUpdateEvent;

        private EnemyInstance _instance;

        private void Awake()
        {
            _instance = new EnemyInstance(_enemyStatsData);
            _statsUpdateEvent.Invoke(_instance);
        }
    }
}