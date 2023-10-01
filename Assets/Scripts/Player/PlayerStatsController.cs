using DefaultNamespace;
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
        [SerializeField] private Inventory _inventory;
        
        private PlayerInstance _instance;

        private void Awake()
        {}

        private void Start()
        {
            var itemBonus = _inventory.GetAllItemBonuses();
            _instance = new PlayerInstance(_playerStatsData, itemBonus);
            _setupStatsEvent.Invoke(_instance);
        }

        public void SetStatByName(Stats.Stats stats, int value)
        {
            //TODO: change to SetBonusByName
            // _instance.SetStatByName(stats, value);
        }
           
    }
}