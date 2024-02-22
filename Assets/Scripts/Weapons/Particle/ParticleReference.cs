using UnityEngine;
using Weapons.RangeWeapons.Particle;

namespace Weapons.Particle
{
    public class ParticleReference : MonoBehaviour
    {
        [SerializeField] private ParticleStatsController _statsController;
        [SerializeField] private ParticleLifeController _particleLifeController;
        [SerializeField] private Rigidbody2D _rigidbody;

        public ParticleStatsController StatsController => _statsController;
        public ParticleLifeController ParticleLifeController => _particleLifeController;
        public Rigidbody2D Rigidbody => _rigidbody;

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