using Stats.Instances;
using Stats.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerStatsController : MonoBehaviour
    {
        [SerializeField] private PlayerStatsData _playerStatsData;
        [SerializeField] private UnityEvent<PlayerStatsInstance> _statsUpdateEvent;

        private PlayerStatsInstance _statsInstance;

        private void Awake()
        {
            _statsInstance = new PlayerStatsInstance(_playerStatsData);
            _statsUpdateEvent.Invoke(_statsInstance);
        }

        private void Start()
        {
        }

        public void SetStatByName(Stats.Stats stats, int value)
        {
            _statsInstance.SetStatByName(stats, value);
        }
        
    }
}