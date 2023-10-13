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
            // DebugPrint("Level up");
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
            // DebugPrint("Update");
        }

        private void DebugPrint(string type)
        {
            var s = $"{type}\nStats: \n";

            foreach (var stat in _instance.CurrentStats)
            {
                s += stat + "\n";
            }

            s += "\nLevel up bonus \n\n";
            
            foreach (var stat in _instance.LevelUpBonus)
            {
                s += stat + "\n";
            }

            s += "\nOutside bonus \n\n";
            
            foreach (var stat in _instance.OutsideBonuses)
            {
                s += stat + "\n";
            }

            s += "\nCurrent bonus\n\n";

            foreach (var stat in _instance.CurrentBonus)
            {
                s += stat + "\n";   
            }
                
            Debug.Log(s);
        }
    }
}