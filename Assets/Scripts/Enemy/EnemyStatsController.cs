using Interface;
using Stats.Instances;
using ScriptableObjects;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyStatsController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private EnemyStatsData _enemyStatsData;
        [SerializeField] private UnityEvent<EnemyInstance> _setupStatsEvent;
        [SerializeField] private UnityEvent<EnemyInstance> _statsUpdateEvent;

        private EnemyInstance _instance;

        private void Awake()
        {
            _instance = new EnemyInstance(_enemyStatsData);
            _setupStatsEvent.Invoke(_instance);
        }

        public void LevelUp()
        {
            _instance.LevelUp();
            _statsUpdateEvent.Invoke(_instance);
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _setupStatsEvent.Invoke(_instance);
            UpdateStatsEventHandler(newInstance);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var playerInstance = (PlayerInstance)newInstance;

            var allClearBonusFromOutside = playerInstance.UnitStatCalculator.ClearBonuses;
            var allPercentBonusFromOutside = playerInstance.UnitStatCalculator.PercentBonuses;

            _instance.AddBonusesFromItems(allClearBonusFromOutside, allPercentBonusFromOutside);
            
            _statsUpdateEvent.Invoke(_instance);
        }
    }
}