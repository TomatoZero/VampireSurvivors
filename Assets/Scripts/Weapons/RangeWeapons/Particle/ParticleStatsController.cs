using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.RangeWeapons.Particle
{
    public class ParticleStatsController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<ObjectInstance> _setupParticle;
        [SerializeField] private UnityEvent<ObjectInstance> _updateParticle;

        public void Setup(WeaponInstance instance)
        {
            _setupParticle.Invoke(instance);
        }

        public void UpdateStats(WeaponInstance instance)
        {
            _updateParticle.Invoke(instance);
        }
    }
}