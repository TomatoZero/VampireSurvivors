using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;
using Weapons.RangeWeapons.Particle;

namespace Weapons.Particle.Spawner
{
    public class FallParticleSpawner : ParticleSpawner
    {
        private WeaponInstance _instance;
        
        public override void Shoot(Vector2 mousePosition)
        {
            var particle = Spawn();
            SetupParticle(particle.StatsController);
            
            particle.transform.position = mousePosition;
            particle.Enable();
        }

        private void SetupParticle(ParticleStatsController statsController)
        {
            statsController.UpdateStats(_instance);
        }
        
        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _instance = (WeaponInstance)newInstance;
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _instance = (WeaponInstance)newInstance;
        }
    }
}