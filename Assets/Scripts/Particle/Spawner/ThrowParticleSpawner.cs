using Particle.ParticleReferences;
using Stats.Instances.PowerUp;
using UnityEngine;

namespace Particle.Spawner
{
    public class ThrowParticleSpawner : ParticleSpawner
    {
        private WeaponInstance _instance;

        public override void Shoot(Vector3 mousePosition)
        {
            var particle = (ThrowParticleReference)Spawn();
            SetupParticle(particle.StatsController);

            var direction = (mousePosition - transform.position).normalized;
            
            particle.transform.position = transform.position;
            particle.Enable();
            particle.MoveController.Move(direction);
        }
    }
}