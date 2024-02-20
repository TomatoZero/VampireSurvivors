using System.Collections;
using Interface;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;
using Weapons.Particle;
using Weapons.RangeWeapons.Particle;

namespace Weapons.RangeWeapons
{
    public abstract class RangeWeaponShootController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _particlesParent;
        [SerializeField] private UnityEvent _startTimerEvent;
        [SerializeField] private UnityEvent _shootEvent;

        private WeaponInstance _instance;
        private int _amount;

        private Queue _unusedParticles;

        private protected GameObject Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }
        private protected Transform ParticlesParent
        {
            get => _particlesParent;
            set => _particlesParent = value;
        }
        private protected WeaponInstance Instance
        {
            get => _instance;
            set => _instance = value;
        }
        private protected int Amount
        {
            get => _amount;
            set => _amount = value;
        }
        private protected UnityEvent StartTimerEvent
        {
            get => _startTimerEvent;
            set => _startTimerEvent = value;
        }

        private protected virtual void Awake()
        {
        }

        public abstract void Shoot();

        private protected virtual void CreateInstance()
        {
            if (_unusedParticles is null)
                _unusedParticles = new Queue();

            if (_unusedParticles.Count == 0)
            {
                var instance = Instantiate(_prefab, transform.position, Quaternion.identity, _particlesParent);
                SetUpParticle(instance);
            }
            else
            {
                var particle = (GameObject)_unusedParticles.Dequeue();
                UpdateParticle(particle);
            }
            
            _shootEvent.Invoke();
        }

        private protected virtual void SetUpParticle(GameObject instance)
        {
            var particle = instance.GetComponent<ParticleStatsController>();
            var lifeController = instance.GetComponent<ParticleLifeController>();

            lifeController.DestroyParticleEvent += ParticleDieEventHandler;
            particle.Setup(_instance);
        }

        private protected virtual void UpdateParticle(GameObject instance)
        {
            var particle = instance.GetComponent<ParticleStatsController>();
            instance.SetActive(true);
            instance.transform.position = transform.position;
            particle.UpdateStats(_instance);
        }

        private protected virtual void ParticleDieEventHandler(GameObject particle)
        {
            _unusedParticles.Enqueue(particle);
        }

        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _instance = (WeaponInstance)newInstance;
            _amount = (int)_instance.GetStatByName(Stats.Stats.Amount).Value;
            _startTimerEvent.Invoke();
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _instance = (WeaponInstance)newInstance;
            _amount = (int)_instance.GetStatByName(Stats.Stats.Amount).Value;
        }
    }
}