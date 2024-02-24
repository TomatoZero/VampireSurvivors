using UnityEngine;

namespace Particle.ParticleReferences
{
    public class ParticleReference : MonoBehaviour
    {
        [SerializeField] private ParticleStatsController _statsController;
        [SerializeField] private ParticleLifeController _particleLifeController;
        [SerializeField] private Rigidbody _rigidbody;

        public ParticleStatsController StatsController => _statsController;
        public ParticleLifeController ParticleLifeController => _particleLifeController;
        public Rigidbody Rigidbody => _rigidbody;

        private void Start()
        {
            _particleLifeController.SetReferenceScript(this);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}