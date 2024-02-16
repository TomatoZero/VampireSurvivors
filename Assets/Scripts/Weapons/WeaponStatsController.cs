using System.Collections.Generic;
using Interface;
using Stats.Instances;
using Stats.Instances.PowerUp;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons
{
    public class WeaponStatsController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private WeaponStatsData _statsData;
        [SerializeField] private UnityEvent<WeaponInstance> _setupStatData;
        [SerializeField] private UnityEvent<WeaponInstance> _updateStatData;

        private WeaponInstance _instance;

        public WeaponInstance Instance => _instance;

        private void Awake()
        {
            if (_statsData is null)
            {
                return;
            }

            _instance = new WeaponInstance(_statsData);
        }

        public void LevelUp()
        {
            _instance.LevelUp();
            _updateStatData.Invoke(_instance);
        }

        public void SetupStatEventHandler(ObjectInstance playerInstance)
        {
            _setupStatData.Invoke(_instance);
            UpdateStatsEventHandler(playerInstance);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var playerInstance = (PlayerInstance)newInstance;

            var allClearBonusFromOutside = playerInstance.PlayerStatCalculator.ClearBonuses;
            var allPercentBonusFromOutside = playerInstance.PlayerStatCalculator.PercentBonuses;

            _instance.AddBonusesFromItems(allClearBonusFromOutside, allPercentBonusFromOutside);
            
            _updateStatData.Invoke(_instance);
        }

        private string GetDict(Dictionary<Stats.Stats, float> dictionary)
        {
            var str = "Percent";
            foreach (var bonus in dictionary)
            {
                str += $"{bonus.Key} {bonus.Value}\n";
            }

            return str;
        }
    }
}