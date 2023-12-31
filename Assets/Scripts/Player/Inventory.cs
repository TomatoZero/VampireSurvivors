﻿using System.Collections.Generic;
using System.Linq;
using Interface;
using Stats;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Player
{
    public class Inventory : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private UnityEvent _updateStatsEvent;
        [SerializeField] private UnityEvent _endSetupStatsEvent;
        [SerializeField] private UnityEvent<List<WeaponInstance>, List<ItemInstance>> _displayCurrentItemsEvent;
        [SerializeField] private List<WeaponStatsController> _weapons;

        private List<ItemInstance> _items;

        private delegate void StatsUpdate(PlayerInstance instance);

        private event StatsUpdate SetupStatEvent;
        private event StatsUpdate UpdateStatEvent;

        private PlayerInstance _playerInstance;

        public List<WeaponStatsController> Weapons => _weapons;
        public List<ItemInstance> Items => _items;


        private void Awake()
        {
            _items = new List<ItemInstance>();
        }

        private void OnEnable()
        {
            foreach (var weapon in _weapons)
            {
                SetupStatEvent += weapon.SetupStatEventHandler;
                UpdateStatEvent += weapon.UpdateStatsEventHandler;
            }
        }

        public void AddItem(ItemInstance item)
        {
            item.UpdateClearAndPercentStats();
            _items.Add(item);
            _updateStatsEvent.Invoke();
            _endSetupStatsEvent.Invoke();
            DisplayCurrentItems();
        }

        public void AddWeapon(WeaponStatsController weapon)
        {
            AddWeapon(weapon, _playerInstance);
            _endSetupStatsEvent.Invoke();
            DisplayCurrentItems();
        }

        public void LevelUpItem(string name)
        {
            var levelUpItem = (from item in _items
                where item.StatsData.Name == name
                select item).First();

            levelUpItem.LevelUp();
            
            _updateStatsEvent.Invoke();
            _endSetupStatsEvent.Invoke();
            DisplayCurrentItems();
        }

        public void LevelUpWeapon(string name)
        {
            var levelUpWeapon = (from weapon in _weapons
                where weapon.Instance.StatsData.Name == name
                select weapon).First();

            levelUpWeapon.LevelUp();
            _endSetupStatsEvent.Invoke();
            DisplayCurrentItems();
        }

        public void EndSetupStats()
        {
            _endSetupStatsEvent.Invoke();
        }

        public (Dictionary<Stats.Stats, float> allClearBonus, Dictionary<Stats.Stats, float> allPercentBonus) GetAllItemBonuses()
        {
            var allClearBonus = new Dictionary<Stats.Stats, float>();
            var allPercentBonus = new Dictionary<Stats.Stats, float>();
            
            foreach (var item in _items)
            {
                foreach (var clearBonus in item.StatsCalculator.ClearBonuses)
                {
                    if (allClearBonus.ContainsKey(clearBonus.Key))
                        allClearBonus[clearBonus.Key] += clearBonus.Value;
                    else
                        allClearBonus[clearBonus.Key] = clearBonus.Value;
                }

                foreach (var percentBonus in item.StatsCalculator.PercentBonuses)
                {
                    if (allPercentBonus.ContainsKey(percentBonus.Key))
                        allPercentBonus[percentBonus.Key] += percentBonus.Value;
                    else
                        allPercentBonus[percentBonus.Key] = percentBonus.Value;
                }
            }
            return (allClearBonus, allPercentBonus);
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _playerInstance = (PlayerInstance)newInstance;
            SetupStatEvent?.Invoke(_playerInstance);
            DisplayCurrentItems();
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _playerInstance = (PlayerInstance)newInstance;
            UpdateStatEvent?.Invoke(_playerInstance);
        }

        private void AddWeapon(WeaponStatsController weapon, PlayerInstance playerInstance)
        {
            _weapons.Add(weapon);

            SetupStatEvent += weapon.SetupStatEventHandler;
            UpdateStatEvent += weapon.UpdateStatsEventHandler;

            weapon.SetupStatEventHandler(playerInstance);
        }

        private void DisplayCurrentItems()
        {
            var weapons = new List<WeaponInstance>();

            foreach (var weapon in _weapons)
            {
                weapons.Add(weapon.Instance);
            }

            _displayCurrentItemsEvent.Invoke(weapons, _items);
        }
    }
}