using System;
using System.Collections.Generic;
using Interface;
using Stats;
using Stats.Instances;
using Stats.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace DefaultNamespace
{
    public class Inventory : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private UnityEvent _updateStatsEvent;
        
        [SerializeField] private List<WeaponStatsController> _weapons;
        [SerializeField] private List<ObjectStatsData> _itemsData;
        [SerializeField] private List<ItemInstance> _items;
        
        private delegate void StatsUpdate(PlayerInstance instance);

        private event StatsUpdate SetupStatEvent;
        private event StatsUpdate UpdateStatEvent;

        private void Awake()
        {
            foreach (var data in _itemsData)
            {
                AddItem(new ItemInstance(data));
            }
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
            _items.Add(item);
            _updateStatsEvent.Invoke();
        }

        public void AddWeapon(WeaponStatsController weapon, PlayerInstance playerInstance)
        {
            _weapons.Add(weapon);
            weapon.SetupStatEventHandler(playerInstance);
        }

        public void LevelUpItem(int id)
        {
            _items[id].LevelUp();
        }

        public void LevelUpWeapon(int id)
        {
            _weapons[id].LevelUp();
        }

        public List<StatData> GetAllItemBonuses()
        {
            var allBonus = new List<StatData>();

            foreach (var item in _items)
            {
                foreach (var itemCurrentStat in item.CurrentStats)
                {
                    var statId = FindStatIdInList(allBonus, itemCurrentStat.Stat);
                    
                    if(statId == -1)
                    {
                        allBonus.Add((StatData)itemCurrentStat.Clone());
                        continue;
                    }
                    
                    allBonus[statId].Value += itemCurrentStat.Value;
                }
            }
            
            return allBonus;
        }

        private int FindStatIdInList(List<StatData> list, Stats.Stats stat)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].Stat == stat) return i;
            }

            return -1;
        }
        
        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            SetupStatEvent?.Invoke((PlayerInstance)newInstance);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            UpdateStatEvent?.Invoke((PlayerInstance)newInstance);
        }
    }
}