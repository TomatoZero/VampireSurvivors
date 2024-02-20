using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.StaticClass;
using Player;
using Stats.Instances;
using ScriptableObjects;
using UI.Structs;
using UnityEngine;
using UnityEngine.Events;
using Weapons;
using Random = System.Random;

namespace DefaultNamespace
{
    public class ItemHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<BonusData[]> _showUpgradeEvent;
        [SerializeField] private UnityEvent _endSetupStats;
        [SerializeField] private LevelWeaponsAndItems _weaponsAndItems;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _inventoryObject;
        [SerializeField] private LoadFromAssetBundle _loadFromAsset;
        [SerializeField] private WeaponsOnLevel _weaponsOnLevel;

        private List<ObjectInstance> _currentWeaponAndItems;
        private List<ObjectStatsData> _newerUsed;

        private void Start()
        {
            GetCurrentWeaponsAndItems();

            _newerUsed = new List<ObjectStatsData>();

            foreach (var item in _weaponsAndItems.Items)
            {
                _newerUsed.Add(item);
            }

            foreach (var item in _weaponsAndItems.Weapons)
            {
                if (IsContainInWeapons(item)) continue;
                _newerUsed.Add(item);
            }

            return;

            bool IsContainInWeapons(ObjectStatsData item)
            {
                foreach (var weapon in _inventory.Weapons)
                {
                    if (weapon.StatsController.Instance.StatsData.Name == item.Name) return true;
                }

                return false;
            }
        }
        
        public void UpgradeEventHandler(BonusData item)
        {
            if (item.StatsData == null)
            {
                _endSetupStats.Invoke();
                return;
            }
            
            GetCurrentWeaponsAndItems();
            foreach (var instance in _currentWeaponAndItems)
            {
                if (instance.StatsData.Name == item.StatsData.Name)
                {
                    LevelUpItem(item);
                    return;
                }
            }

            var removeItem = (from itemName in _newerUsed
                where itemName.Name == item.StatsData.Name
                select itemName).First();

            if (removeItem is null)
            {
                throw new NullReferenceException(
                    "Upgrade item have to ber some where in _currentWeaponAndItems or in _newerUsed");
            }
            
            _newerUsed.Remove(removeItem);
            AddNewItem(item);
        }

        public void ShowLevelUps()
        {
            if (_currentWeaponAndItems.Count > 6)
            {
                Debug.LogWarning("Count weapon and items more then must be");
                return;
            }

            var canBeUpgraded = FindPossibleUpgrade();
            var upgrades = GetUpgrade(canBeUpgraded);

            _showUpgradeEvent.Invoke(upgrades);
        }

        private void GetCurrentWeaponsAndItems()
        {
            _currentWeaponAndItems = new List<ObjectInstance>();

            foreach (var weapon in _inventory.Weapons)
            {
                _currentWeaponAndItems.Add(weapon.StatsController.Instance);
            }
            
            foreach (var item in _inventory.Items)
            {
                _currentWeaponAndItems.Add(item);
            }
        }
        
        private void LevelUpItem(BonusData item)
        {
            if (item.IsWeapon) _inventory.LevelUpWeapon(item.StatsData.Name);
            else _inventory.LevelUpItem(item.StatsData.Name);
        }

        private void AddNewItem(BonusData item)
        {
            if (item.IsWeapon)
            {
                // var prefab = _loadFromAsset.LoadPrefab("weapons",
                //     $"{RemoveWhitespaces.RemoveWhitespacesUsingRegex(item.StatsData.Name)}.prefab");
                
                var prefab = _weaponsOnLevel.LoadPrefab($"{RemoveWhitespaces.RemoveWhitespacesUsingRegex(item.StatsData.Name)}");
               AddNewWeapon(prefab);
            }
            else
            {
                var itemInstance = new ItemInstance(item.StatsData);
                _inventory.AddItem(itemInstance);
            }
        }

        private void AddNewWeapon(GameObject prefab)
        {
            var weapon = Instantiate(prefab, _inventoryObject.position, Quaternion.identity, _inventoryObject);
            var statController = weapon.GetComponent<WeaponReferences>();
            _inventory.AddWeapon(statController);
        }

        private List<BonusData> FindPossibleUpgrade()
        {
            var canBeUpgraded = FindPossibleUpgradeInInventory();
            canBeUpgraded.AddRange(FindPossibleUpgradeInNewItem());
            return canBeUpgraded;
        }

        private List<BonusData> FindPossibleUpgradeInInventory()
        {
            GetCurrentWeaponsAndItems();
            var canBeUpgraded = new List<BonusData>();

            foreach (var item in _currentWeaponAndItems)
            {
                if (item.CurrentLvl >= item.MaxLevel) continue;

                canBeUpgraded.Add(new BonusData(item.StatsData, item.CurrentLvl + 1));
            }
            
            return canBeUpgraded;
        }

        private List<BonusData> FindPossibleUpgradeInNewItem()
        {
            var canBeUpgraded = new List<BonusData>();

            foreach (var data in _newerUsed)
            {
                canBeUpgraded.Add(new BonusData(data, 0));
            }

            return canBeUpgraded;
        }

        private BonusData[] GetUpgrade(List<BonusData> canBeUpgraded)
        {
            Shuffle(canBeUpgraded);

            var upgrade = new BonusData[3];

            for (int i = 0; i < canBeUpgraded.Count && i < 3; i++)
            {
                upgrade[i] = canBeUpgraded[i];
            }

            return upgrade;
        }

        private void Shuffle<T>(IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}