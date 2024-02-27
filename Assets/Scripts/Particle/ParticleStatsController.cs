using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;

namespace Particle
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