using System.Collections.Generic;
using DefaultNamespace;
using Stats.Instances.PowerUp;
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
        {
        }

        private void Start()
        {
            var itemBonus = _inventory.GetAllItemBonuses();
            _instance = new PlayerInstance(_playerStatsData, itemBonus.allClearBonus, itemBonus.allPercentBonus);
            _setupStatsEvent.Invoke(_instance);
        }

        public void LevelUp()
        {
            _instance.LevelUp();
            _statsUpdateEvent.Invoke(_instance);
        }

        public void UpdateStatsEventHandler()
        {
            var itemBonus = _inventory.GetAllItemBonuses();
            
            _instance.AddBonusesFromItems(itemBonus.allClearBonus, itemBonus.allPercentBonus);
            _statsUpdateEvent.Invoke(_instance);
        }
    }
}