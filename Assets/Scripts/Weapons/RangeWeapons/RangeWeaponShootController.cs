using Interface;
using Stats.Instances;
using UnityEngine;
using Weapons.RangeWeapons.Particle;

namespace Weapons.RangeWeapons
{
    public abstract class RangeWeaponShootController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _particlesParent;

        private int _amount;
        public int Amount => _amount;
        

        public abstract void Shoot();

        private protected abstract Vector2 GetRandomShootDirection();

        private protected virtual void CreateInstance(Vector2 moveDirection)
        {
            var instance = Instantiate(_prefab, _particlesParent);
            SetUpInstance(instance);
        }

        private protected virtual void SetUpInstance(GameObject instance)
        {
            var particleMoveController = instance.GetComponent<ParticleMoveController>();
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;

            _amount = (int)weaponInstance.GetStatByName(Stats.Stats.Amount).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;

            _amount = (int)weaponInstance.GetStatByName(Stats.Stats.Amount).Value;
        }
    }
}