using Stats.Instances;
using Stats.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerStatsController : MonoBehaviour
    {
        [SerializeField] private PlayerStatsData _playerStatsData;
        [SerializeField] private UnityEvent<PlayerInstance> _setupStatsEvent;
        [SerializeField] private UnityEvent<PlayerInstance> _statsUpdateEvent;

        private PlayerInstance _instance;

        private void Awake()
        {
            _instance = new PlayerInstance(_playerStatsData);
            _setupStatsEvent.Invoke(_instance);
        }

        private void Start()
        {
        }

        public void SetStatByName(Stats.Stats stats, int value)
        {
            //TODO: change to SetBonusByName
            // _instance.SetStatByName(stats, value);
        }
        
    }
}