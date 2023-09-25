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

        private WeaponInstance _instance;

        private int _amount;

        protected int Amount => _amount;


        public abstract void Shoot();

        private protected virtual void CreateInstance()
        {
            var instance = Instantiate(_prefab, transform.position, Quaternion.identity, _particlesParent);
            SetUpParticle(instance);
        }

        private protected virtual void SetUpParticle(GameObject instance)
        {
            var particle = instance.GetComponent<ParticleStatsController>();
            particle.Setup(_instance);
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _instance = (WeaponInstance)newInstance;
            _amount = (int)_instance.GetStatByName(Stats.Stats.Amount).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _instance = (WeaponInstance)newInstance;
            _amount = (int)_instance.GetStatByName(Stats.Stats.Amount).Value;
        }
    }
}