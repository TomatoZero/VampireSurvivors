using Stats.Instances;
using Stats.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerStatsController : MonoBehaviour
    {
        [SerializeField] private PlayerStatsData _playerStatsData;
        [SerializeField] private UnityEvent<PlayerStats> _statsUpdateEvent;

        private PlayerStats _stats;

        private void Awake()
        {
            _stats = new PlayerStats(_playerStatsData);
            _statsUpdateEvent.Invoke(_stats);
        }

        private void Start()
        {
        }

        public void SetStatByName(Stats.Stats stats, int value)
        {
            _stats.SetStatByName(stats, value);
        }
        
    }
}