using Interface;
using ScriptableObjects;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.StatsController
{
    public abstract class BaseWeaponStatController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private WeaponStatsData _statsData;
        [SerializeField] private protected UnityEvent<WeaponInstance> _setupStatData;
        [SerializeField] private protected UnityEvent<WeaponInstance> _updateStatData;

        protected WeaponInstance _instance;

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

        public abstract void SetupStatEventHandler(ObjectInstance newInstance);
        public abstract void UpdateStatsEventHandler(ObjectInstance newInstance);
    }
}