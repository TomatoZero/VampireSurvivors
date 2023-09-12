using Stats.Instances;
using Stats.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons
{
    public class WeaponStatsController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private WeaponStatsData _statsData;
        [SerializeField] private UnityEvent<WeaponInstance> _updateStatData;

        private WeaponInstance _instance;

        private void Awake()
        {
            _instance = new WeaponInstance(_statsData);
            _updateStatData.Invoke(_instance);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var playerInstance = (PlayerInstance)newInstance;
            var currentStats = playerInstance.CurrentStats;

            foreach (var statData in currentStats)
            {
                _instance.SetStat(statData);   
            }
            
            _updateStatData.Invoke(_instance);
        }
    }
}