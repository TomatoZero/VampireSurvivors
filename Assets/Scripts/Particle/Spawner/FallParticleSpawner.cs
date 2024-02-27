using Stats.Instances.PowerUp;
using UnityEngine;

namespace Particle.Spawner
{
    public class FallParticleSpawner : ParticleSpawner
    {
        private WeaponInstance _instance;

        public override void Shoot(Vector3 mousePosition)
        {
            var particle = Spawn();
            SetupParticle(particle.StatsController);

            particle.transform.position = mousePosition;
            
            particle.Enable();
        }
    }
}