using System.Collections.Generic;
using Interface;
using Particle.ParticleReferences;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;

namespace Particle.Spawner
{
    public abstract class ParticleSpawner : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private Transform _particleFather;
        [SerializeField] private GameObject _prefab;

        private Queue<ParticleReference> _particles;
        private WeaponInstance _instance;

        private void Start()
        {
            _particles = new Queue<ParticleReference>();
        }

        public abstract void Shoot(Vector3 mousePosition);

        public ParticleReference Spawn()
        {
            return _particles.TryDequeue(out ParticleReference particle) ? particle : CrateInstance();
        }

        private ParticleReference CrateInstance()
        {
            var instance = Instantiate(_prefab, _particleFather);
            var reference = instance.GetComponent<ParticleReference>();

            reference.Disable();

            reference.ParticleLifeController.ParticleDieEvent += ParticleDieEventHandler;
            return reference;
        }

        protected void SetupParticle(ParticleStatsController statsController)
        {
            statsController.UpdateStats(_instance);
        }
        
        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _instance = (WeaponInstance)newInstance;
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _instance = (WeaponInstance)newInstance;
        }

        private protected virtual void ParticleDieEventHandler(ParticleReference particle)
        {
            particle.Disable();
            _particles.Enqueue(particle);
        }
    }
}