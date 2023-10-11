using Interface;
using Stats.Instances;
using Stats.ScriptableObjects;
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
            var currentStats = playerInstance.CurrentStats;

            foreach (var statData in currentStats)
            {
                if (statData.Stat == Stats.Stats.Luck) _instance.SetStatByName(statData.Stat, statData.Value);
            }

            foreach (var bonus in playerInstance.CurrentBonus)
            {
                _instance.AddValueToBonus(bonus.Stat, bonus.Value);
            }

            _updateStatData.Invoke(_instance);
        }
    }
}