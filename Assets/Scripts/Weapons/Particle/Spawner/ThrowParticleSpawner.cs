using Stats.Instances.PowerUp;
using UnityEngine;

namespace Weapons.Particle.Spawner
{
    public class ThrowParticleSpawner : ParticleSpawner
    {
        private WeaponInstance _instance;

        public override void Shoot(Vector2 mousePosition)
        {
            var particle = Spawn();
            SetupParticle(particle.StatsController);

            var direction = (mousePosition - (Vector2)transform.position).normalized;
            
            particle.transform.position = transform.position;
            
            particle.Enable();
            particle.Rigidbody.AddForce(direction);
        }
    }
}