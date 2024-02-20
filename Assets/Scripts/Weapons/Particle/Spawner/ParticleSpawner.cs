using System;
using System.Collections.Generic;
using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons.Particle.Spawner
{
    public abstract class ParticleSpawner : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private GameObject _prefab;

        private Queue<ParticleReference> _particles;

        private void Start()
        {
            _particles = new Queue<ParticleReference>();
        }

        public abstract void Shoot(Vector2 mousePosition);

        public ParticleReference Spawn()
        {
            return _particles.TryDequeue(out ParticleReference particle) ? particle : CrateInstance();
        }

        private ParticleReference CrateInstance()
        {
            var instance = Instantiate(_prefab, transform);
            var reference = instance.GetComponent<ParticleReference>();
            
            reference.Disable();
            
            reference.ParticleLifeController.ParticleDieEvent += ParticleDieEventHandler;
            return reference;
        }

        public abstract void SetupStatEventHandler(ObjectInstance newInstance);
        public abstract void UpdateStatsEventHandler(ObjectInstance newInstance);

        
        private protected virtual void ParticleDieEventHandler(ParticleReference particle)
        {
            particle.Disable();
            _particles.Enqueue(particle);
        }
    }
}