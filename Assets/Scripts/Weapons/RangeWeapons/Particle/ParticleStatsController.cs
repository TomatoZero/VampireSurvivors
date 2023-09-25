using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.RangeWeapons.Particle
{
    public class ParticleStatsController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<ObjectInstance> _setupParticle;

        public void Setup(WeaponInstance instance)
        {
            _setupParticle.Invoke(instance);
        }
    }
}